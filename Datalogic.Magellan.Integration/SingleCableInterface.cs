using System.IO.Ports;
using Microsoft.Extensions.Logging;

namespace DataLogic.Magellan.Integration
{
    /// <summary>
    /// Used to communicate with the scale over a single RS-232/ETH Cable.
    /// </summary>
    public class SingleCableInterface : IDisposable
    {
        private readonly ILogger? _logger;
        private readonly SerialPort _serialPort;
        /// <summary>
        /// Event raised when scan data is received by the scanner.
        /// </summary>
        public event ScanDataReceivedEventArgs? OnScanDataReceived;
        /// <summary>
        /// Event raised after we request (and then receive) weight data from the scale.
        /// </summary>
        public event WeightDataReceivedEventArgs? OnWeightDataReceived;
        /// <summary>
        /// The command used to send to the scales to request the status (and weight) of the scales.
        /// </summary>
        public const string RequestWeightCommand = "S14\r\n";
        /// <summary>
        /// Sends a weight request command to the scales which returns the status of the scale, along with the weight if in a valid state.
        /// </summary>
        /// <returns></returns>
        public async Task SendRequestWeightCommand()
        {
            _logger?.LogInformation($@"Sending weight request {RequestWeightCommand}");
            try
            {
                await Task.Run(() => _serialPort.Write(RequestWeightCommand));
            }
            catch (Exception ex)
            {
                _logger?.LogError("Error Sending Weight Request!\r\n" +
                                  $"Request: {RequestWeightCommand}\r\n" +
                                  $"Error: {ex.ToString()}");
            }
        }
        /// <summary>
        /// Instantiate a new connection with the scale.
        /// Call <see cref="OpenPort"/> to connect to the serial port to start listening. 
        /// </summary>
        /// <param name="serialPortConfiguration"></param>
        /// <param name="logger"></param>
        public SingleCableInterface(DefaultSerialPortConfiguration serialPortConfiguration, ILogger? logger = null)
        {
            _logger = logger;

            // Instantiate the communications port
            _serialPort = new SerialPort(
                serialPortConfiguration.SerialPortName,
                serialPortConfiguration.BaudRate,
                serialPortConfiguration.Parity,
                serialPortConfiguration.DataBits,
                serialPortConfiguration.StopBits
            );

            this._serialPort.NewLine = "\r";
            this._serialPort.Handshake = Handshake.None;
            this._serialPort.RtsEnable = true;
            this._serialPort.DtrEnable = true;
            this._serialPort.WriteTimeout = 500;

            _serialPort.DataReceived +=  OnSerialDataReceived;
        }

        /// <summary>
        /// Open the port for communication.
        /// </summary>
        public void OpenPort()
        {
            // Open the port for communications
            _serialPort.Open();
        }

        public void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            _logger?.LogInformation("OnSerialDataReceived event fired");

            // read up to the newline property ('\r')
            var responseData = _serialPort.ReadLine();

            try
            {
                ParseResponse(responseData);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error parsing response {responseData}!");
            }
        }

        /// <summary>
        /// Parse the raw string response from the SerialPort into meaningful data.
        /// Raises the <see cref="OnWeightDataReceived"/> or <see cref="OnScanDataReceived"/> depending on the data type.
        /// </summary>
        /// <param name="responseString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Exception"></exception>
        private void ParseResponse(string responseString)
        {
            _logger?.LogInformation($@"Starting to parse response {responseString}");

            if (responseString.StartsWith("S08")) // barcode scan data
            {
                // response typical > S08{Label_ID}{DATA}\r
                // example response S08B39289510002308107
                //
                // --------------------------
                // S08 (command)
                // B3 = Barcode Type ID
                // 9289510002308107 - barcode data. (may or may not include check digit)
                // --------------------------
                //
                // the value of the {LABEL_ID} will be 2 Chars long,
                // except in a couple of scenarios where its only 1 char

                BarcodeType barcodeType;
                // most barcode type prefixes are 2 chars long, with 5 exceptions as per below.
                var typeIdLength = 2;

                // with or without check-digit
                if (responseString.Substring(3, 1) == "A")
                {
                    typeIdLength = 1;
                    barcodeType = BarcodeType.UPC_A;
                }
                else if (responseString.Substring(3, 1) == "E")
                {
                    typeIdLength = 1;
                    barcodeType = BarcodeType.UPC_E;
                }
                // Check to make sure its not an EAN_8 barcode, as per below.
                else if (responseString.Substring(3, 1) == "F" && responseString.Substring(3, 2) != "FF")
                {
                    typeIdLength = 1;
                    barcodeType = BarcodeType.EAN_13;
                }
                else if (responseString.Substring(3, 1) == "%")
                {
                    barcodeType = BarcodeType.CodaBar;
                }
                else if (responseString.Substring(3, 1) == "&")
                {
                    typeIdLength = 1;
                    barcodeType = BarcodeType.Code93;
                }
                else
                {
                    var typeString = responseString.Substring(3, 2).ToUpper();
                    switch (typeString)
                    {
                        case "FF":
                            barcodeType = BarcodeType.EAN_8;
                            break;
                        case "R4":
                            barcodeType = BarcodeType.DataBar;
                            break;
                        case "RX":
                            barcodeType = BarcodeType.DataBarExpanded;
                            break;
                        case "B1":
                            barcodeType = BarcodeType.Code39;
                            break;
                        case "B2":
                            barcodeType = BarcodeType.ITF;
                            break;
                        case "B3":
                            barcodeType = BarcodeType.Code128;
                            break;
                        case "QR":
                            barcodeType = BarcodeType.QR_Code;
                            break;
                        case "DM":
                            barcodeType = BarcodeType.DataMatrix;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(typeString);
                    }
                }

                // 'S08' + find the starting part of the barcode data based on the length of the 'TypeID' chars (either 1 or 2)
                var startIndex = 3 + typeIdLength;

                // Extract the label data from the response.
                var data = responseString.Substring(startIndex, responseString.Length - startIndex);

                // Invoke the event handler.
                OnScanDataReceived?.Invoke(new ScanSerialDataResponse()
                {
                    BarcodeData = data,
                    IsValid = true,
                    Message = "",
                    BarcodeType = barcodeType,
                    RawSerialResponse = responseString
                });
            }
            else if (responseString.StartsWith("S14")) // weight response data
            {
                var success = false;
                var message = string.Empty;
                var weightGrams = 0;

                // example response S14400260
                // --------------------------
                // S14 (command)
                // 4 = Status
                // 00260 - 5 digit zero padded weight in grams
                // --------------------------

                var status = Convert.ToInt32(responseString.Substring(3, 1));

                switch (status)
                {
                    case 0:
                        message = "Scale Not Ready";
                        break;
                    case 1:
                        message = "Scale Unstable";
                        break;
                    case 2:
                        message = "Scale Over Capacity";
                        break;
                    case 3:
                        message = "Stable Zero Weight";
                        break;
                    case 4:
                        message = "Stable Non-Zero Weight";
                        success = true;
                        break;
                    case 5:
                        message = "Scale Under Zero!";
                        break;
                }

                if (success)
                {
                    var weightString = responseString.Substring(4, 5);
                    // E.g. 00260  for 260 grams.   (0.260kg)
                    // 10215       for 10,215 grams (10.215kg)
                    weightGrams = Convert.ToInt32(weightString);
                }

                // Invoke the event handler
                OnWeightDataReceived?.Invoke(new WeightSerialDataResponse
                {
                    IsValid = success,
                    WeightGrams = weightGrams,
                    Message = message,
                    RawSerialResponse = responseString
                });
            }
            else
            {
                throw new Exception($"Error parsing response! {responseString}");
            }
        }

        public void Dispose()
        {
            if (_serialPort is { IsOpen: true })
                _serialPort.Close();

            _serialPort.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}