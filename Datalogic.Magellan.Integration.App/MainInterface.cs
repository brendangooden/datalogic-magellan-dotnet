using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using DataLogic.Magellan.Integration.App.Properties;
using Microsoft.VisualBasic.CompilerServices;
using WindowsInput;
using WindowsInput.Native;
using Microsoft.Win32;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace DataLogic.Magellan.Integration.App
{
    public partial class MainInterface : Form
    {
        private readonly ILogger _fileErrorLogger = new FileErrorLogger(Path.Combine(Directory.GetCurrentDirectory(), "Log.txt"));
        private readonly InputSimulator _inputSimulator = new InputSimulator();
        private SingleCableInterface _magellanScale;
        private readonly StringBuilder _currentLogString;

        private double? _lastWeightKg;
        private bool _isWeightStatusChanged;
        private string _lastWeightMessage;

        public const string DataLogicMagellanSeries = "DataLogic Magellan 9xxx Series";

        public const string AppName = "DataLogic.Magellan.Integration.App";

        private readonly List<ScaleDefaults> _scaleDefaults = new List<ScaleDefaults>
        {
            new ()
            {
                ScaleName = DataLogicMagellanSeries,
                StopBits = StopBits.One,
                BaudRate = 9600,
                DataBits = 7,
                Parity = Parity.Odd
            }
        };


        private void SetStartup()
        {
            if (Debugger.IsAttached)
                return; // ignore if debugging. 

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rk is not null && rk.GetValue(AppName) is null)
            {
                Log($"Setting auto-startup for '{Application.ExecutablePath}'");
                rk.SetValue(AppName, Application.ExecutablePath);
            }

            // to remove
            // rk.DeleteValue("DataLogic.Magellan.Integration.App", false);

        }

        public MainInterface()
        {
            InitializeComponent();

            // make it automatically load at startup. 
            SetStartup();

            Load += Form1_Load;

            WindowState = FormWindowState.Minimized;

            FormClosing += Form1_FormClosing;

            _currentLogString = new StringBuilder();

            NotifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
            NotifyIcon1.BalloonTipClicked += NotifyIcon1_DoubleClick;

            notifyIconBtnExit.Click += (sender, args) =>
            {
                Application.Exit();
            };

            ScaleTypeCmbo.Items.AddRange(_scaleDefaults.Cast<object>().ToArray());

            cbxParity.Items.AddRange(new object[]
            {
                Parity.Odd,
                Parity.Even,
                Parity.None
            });

            cbxStopBits.Items.AddRange(new object[]
            {
                StopBits.None,
                StopBits.One,
                StopBits.OnePointFive,
                StopBits.Two
            });

            cbxDataBits.Items.AddRange(new object[] { 4, 5, 6, 7, 8 });

            CheckAndKillOtherInstances();
        }

        private void CheckAndKillOtherInstances()
        {
            // only allow one instance to be running at a time to prevent Serial Port blocking etc.
            try
            {
                var curProcess = Process.GetCurrentProcess();

                var processes = Process.GetProcessesByName(curProcess.ProcessName).Where(a => a.Id != curProcess.Id);

                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
            catch
            {
                // eat.
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCommPorts();
            LoadSettings();
            ShowBalloonTip();
            SizeChanged += Form1_ResizeEnd;
        }

        private void ShowBalloonTip()
        {
            NotifyIcon1.ShowBalloonTip(500, "Scale Integration API Running", "Port " + COMPORTCmbo.Text + ", click for to access settings", ToolTipIcon.Info);
        }

        public void LoadSettings()
        {
            // set the com port first, because as soon as we select the scale type we try and start listening on the COM port.
            if (!string.IsNullOrEmpty(Settings.Default.SelectedComPort))
                COMPORTCmbo.SelectedItem = Settings.Default.SelectedComPort;

            ScaleTypeCmbo.SelectedIndex = 0; // default
        }

        public void SaveSettings()
        {
            Settings.Default.SelectedComPort = (string)COMPORTCmbo.SelectedItem;
            Settings.Default.Save();
        }

        public async Task SendCommand()
        {
            if (_magellanScale is null) return;
            Log("Sending weight request.");
            await _magellanScale.SendRequestWeightCommand();
        }

        private async void HalfSecondTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!SendonTimerChk.Checked || _magellanScale is null) return;

                await SendCommand();

                if (chkDisplayLogOutput.Checked)
                {
                    TextBox1.Text = _currentLogString.ToString();
                    TextBox1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }

        private void FiveSecondTimer_Tick(object sender, EventArgs e) => LoadCommPorts();

        public void LoadCommPorts()
        {
            string[] portNames = SerialPort.GetPortNames();
            if (COMPORTCmbo.Tag != portNames)
            {
                object objectValue = RuntimeHelpers.GetObjectValue(COMPORTCmbo.SelectedItem);
                COMPORTCmbo.Items.Clear();
                string[] strArray = portNames;
                int index = 0;
                while (index < strArray.Length)
                {
                    COMPORTCmbo.Items.Add(strArray[index]);
                    checked { ++index; }
                }
                COMPORTCmbo.Tag = portNames;
                if (COMPORTCmbo.Items.Count == 0)
                    return;
                if (objectValue == null)
                    COMPORTCmbo.SelectedIndex = 0;
                else if (COMPORTCmbo.Items.Contains(RuntimeHelpers.GetObjectValue(objectValue)))
                    COMPORTCmbo.SelectedItem = RuntimeHelpers.GetObjectValue(objectValue);
                else
                    COMPORTCmbo.SelectedIndex = 0;
            }
        }

        public void ConnectScale()
        {
            try
            {
                if (COMPORTCmbo.SelectedItem is null) return;

                _magellanScale?.Dispose();

                var scaleConfig = new DefaultSerialPortConfiguration()
                {
                    SerialPortName = (string)COMPORTCmbo.SelectedItem,
                    BaudRate = Convert.ToInt32(BaudTxt.Text),
                    Parity = (Parity)cbxParity.SelectedItem,
                    StopBits = (StopBits)cbxStopBits.SelectedItem,
                    DataBits = (int)cbxDataBits.SelectedItem
                };

                _magellanScale = new SingleCableInterface(scaleConfig, _fileErrorLogger);

                _magellanScale.OnScanDataReceived += OnScanDataReceived;
                _magellanScale.OnWeightDataReceived += OnWeightDataReceived;

                _magellanScale.OpenPort();

                Log("Listening on " + scaleConfig.SerialPortName + "...");

            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }

        public void Log(string loggedText)
        {
            var msg = $"{DateTime.Now:O} {loggedText}";
            Debug.WriteLine(msg);

            if (chkDisplayLogOutput.Checked)
            {
                _currentLogString.Insert(0, $"{msg}\r\n");
            }
        }

        private void OnWeightDataReceived(WeightSerialDataResponse parsedResponse)
        {
            try
            {
                var message = string.Empty;

                Log($"Read Raw Weight - {parsedResponse.RawSerialResponse}");

                // if the last message is different, means the status has changed.
                if (_lastWeightMessage != parsedResponse.Message)
                {
                    _lastWeightMessage = parsedResponse.Message;
                    _isWeightStatusChanged = true;

                    Log("Scale Status Changed - Clearing Clipboard.");
                    // clear the data if the status changes.
                    this.Invoke(_ => Clipboard.Clear());
                }
                else
                {
                    _isWeightStatusChanged = false;
                }

                if (parsedResponse.IsValid)
                {
                    Log("Successful Parse - Type: " + parsedResponse.ResponseType);

                    if (!string.IsNullOrEmpty(parsedResponse.Message))
                    {
                        Log("Successful Parse - Message: " + parsedResponse.Message);
                    }

                    Log("Successful Parse - Weight: " + parsedResponse.WeightKilograms.Value.ToString("F3") + " kg");
                    // set the weight to the clipboard for the Vend Script to use.
                    message = $"{parsedResponse.WeightKilograms.Value:F3} kg";

                    // only set the weight if the status has changed OR 
                    // we don't currently have a weight set.
                    if (_lastWeightKg is null || _isWeightStatusChanged || _lastWeightKg != parsedResponse.WeightKilograms)
                    {
                        var txt = "VENDOCTOR_WEIGHT_" + parsedResponse.WeightGrams.Value.ToString("D4");

                        Log($"Weight Status Changed - Setting clipboard text: {txt}");

                        _lastWeightKg = parsedResponse.WeightKilograms;

                        this.Invoke(_ => Clipboard.SetText(txt));
                    }
                }
                else
                {
                    message = parsedResponse.Message;
                    Log("Unsuccessful Parse - Message: " + parsedResponse.Message);
                }

                SetWeightDisplay(message);
            }
            catch (Exception ex)
            {
                Log("Error receiving weight data!" + ex.Message);
            }
        }
        private void OnScanDataReceived(ScanSerialDataResponse parsedResponse)
        {
            try
            {
                Log($"Read Raw Scan - {parsedResponse.RawSerialResponse}");

                if (parsedResponse.IsValid)
                {
                    Log("Successful Parse - Type: " + parsedResponse.ResponseType);

                    if (!string.IsNullOrEmpty(parsedResponse.Message))
                    {
                        Log("Successful Parse - Message: " + parsedResponse.Message);
                    }

                    Log("Successful Parse - Barcode Data: " + parsedResponse.BarcodeData);

                    // simulate a keyboard input.
                    // If there's no uppercase letters, we can input as an entire string.

                    if (parsedResponse.BarcodeData == parsedResponse.BarcodeData.ToLower())
                    {
                        _inputSimulator.Keyboard.TextEntry(parsedResponse.BarcodeData);
                    }
                    else
                    {
                        // Otherwise loop through each char and press shift before the uppercase char.
                        foreach (var chr in parsedResponse.BarcodeData)
                        {
                            if (char.IsUpper(chr))
                            {
                                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                            }

                            _inputSimulator.Keyboard.TextEntry(chr);

                            if (char.IsUpper(chr))
                            {
                                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                            }
                        }
                    }
                    
                    // add a newline after we have finished, same as pressing enter on the keyboard.
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                }
                else
                {
                    Log("Unsuccessful Parse - Message: " + parsedResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Log("Error getting scan data!" + ex.Message);
            }
        }

        private void SetWeightDisplay(string weightOrMessage)
        {
            this.Invoke(_ => TextBox2.Text = weightOrMessage);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
                return;
            }

            SaveSettings();
            NotifyIcon1.Dispose();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Minimized;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
                return;

            ShowBalloonTip();
        }

        private void ScaleTypeCmbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var scaleDefault = (ScaleDefaults)ScaleTypeCmbo.SelectedItem;
            BaudTxt.Text = scaleDefault.BaudRate.ToString();
            cbxParity.SelectedItem = scaleDefault.Parity;
            cbxStopBits.SelectedItem = scaleDefault.StopBits;
            cbxDataBits.SelectedItem = scaleDefault.DataBits;

            ConnectScale();
        }

        private void btnSendManualCommand_Click(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Represents default settings based on the selected scale make/model.
    /// </summary>
    public class ScaleDefaults
    {
        public required string ScaleName { get; set; }

        /// <summary>
        /// Default value is 9600
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// Default value is 7
        /// </summary>
        public int DataBits { get; set; } = 7;
        /// <summary>
        /// Default value is One.
        /// </summary>
        public StopBits StopBits { get; set; } = StopBits.One;
        /// <summary>
        /// Default value is Even.
        /// </summary>
        public Parity Parity { get; set; } = Parity.Odd;
    }
}
