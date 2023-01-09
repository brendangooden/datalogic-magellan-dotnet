using Timer = System.Windows.Forms.Timer;

namespace DataLogic.Magellan.Integration.App
{
    partial class MainInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            this.HalfSecondTimer = new System.Windows.Forms.Timer(this.components);
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.COMPORTCmbo = new System.Windows.Forms.ComboBox();
            this.FiveSecondTimer = new System.Windows.Forms.Timer(this.components);
            this.BaudTxt = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIconBtnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox2 = new System.Windows.Forms.Label();
            this.TexttosendTxt = new System.Windows.Forms.TextBox();
            this.btnSendManualCommand = new System.Windows.Forms.Button();
            this.SendonTimerChk = new System.Windows.Forms.CheckBox();
            this.ScaleTypeCmbo = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkDisplayLogOutput = new System.Windows.Forms.CheckBox();
            this.notifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // HalfSecondTimer
            // 
            this.HalfSecondTimer.Enabled = true;
            this.HalfSecondTimer.Interval = 500;
            this.HalfSecondTimer.Tick += new System.EventHandler(this.HalfSecondTimer_Tick);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.BackColor = System.Drawing.Color.White;
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox1.ForeColor = System.Drawing.Color.Silver;
            this.TextBox1.Location = new System.Drawing.Point(0, 303);
            this.TextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(696, 194);
            this.TextBox1.TabIndex = 1;
            // 
            // COMPORTCmbo
            // 
            this.COMPORTCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMPORTCmbo.FormattingEnabled = true;
            this.COMPORTCmbo.Location = new System.Drawing.Point(96, 84);
            this.COMPORTCmbo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.COMPORTCmbo.Name = "COMPORTCmbo";
            this.COMPORTCmbo.Size = new System.Drawing.Size(156, 23);
            this.COMPORTCmbo.TabIndex = 2;
            // 
            // FiveSecondTimer
            // 
            this.FiveSecondTimer.Enabled = true;
            this.FiveSecondTimer.Interval = 5000;
            this.FiveSecondTimer.Tick += new System.EventHandler(this.FiveSecondTimer_Tick);
            // 
            // BaudTxt
            // 
            this.BaudTxt.Location = new System.Drawing.Point(96, 116);
            this.BaudTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BaudTxt.Name = "BaudTxt";
            this.BaudTxt.Size = new System.Drawing.Size(156, 23);
            this.BaudTxt.TabIndex = 4;
            this.BaudTxt.Text = "9600";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.Black;
            this.Label2.Location = new System.Drawing.Point(14, 87);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 15);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "COM Port:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(10, 118);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(67, 15);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "BAUD Rate:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label5.ForeColor = System.Drawing.Color.LimeGreen;
            this.Label5.Location = new System.Drawing.Point(4, 10);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(294, 31);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "Scale Integration App";
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.ContextMenuStrip = this.notifyIconContextMenu;
            this.NotifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon1.Icon")));
            this.NotifyIcon1.Text = "Scale Integration App";
            this.NotifyIcon1.Visible = true;
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyIconBtnExit});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(94, 26);
            // 
            // notifyIconBtnExit
            // 
            this.notifyIconBtnExit.Name = "notifyIconBtnExit";
            this.notifyIconBtnExit.Size = new System.Drawing.Size(93, 22);
            this.notifyIconBtnExit.Text = "Exit";
            // 
            // TextBox2
            // 
            this.TextBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TextBox2.BackColor = System.Drawing.Color.MintCream;
            this.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox2.Font = new System.Drawing.Font("Courier New", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TextBox2.ForeColor = System.Drawing.Color.Black;
            this.TextBox2.Location = new System.Drawing.Point(18, 221);
            this.TextBox2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(651, 79);
            this.TextBox2.TabIndex = 0;
            this.TextBox2.Text = "0000";
            this.TextBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TexttosendTxt
            // 
            this.TexttosendTxt.Location = new System.Drawing.Point(362, 146);
            this.TexttosendTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TexttosendTxt.Name = "TexttosendTxt";
            this.TexttosendTxt.Size = new System.Drawing.Size(68, 23);
            this.TexttosendTxt.TabIndex = 15;
            // 
            // btnSendManualCommand
            // 
            this.btnSendManualCommand.Location = new System.Drawing.Point(438, 143);
            this.btnSendManualCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSendManualCommand.Name = "btnSendManualCommand";
            this.btnSendManualCommand.Size = new System.Drawing.Size(151, 27);
            this.btnSendManualCommand.TabIndex = 16;
            this.btnSendManualCommand.Text = "Send Manual Command";
            this.btnSendManualCommand.UseVisualStyleBackColor = true;
            this.btnSendManualCommand.Click += new System.EventHandler(this.btnSendManualCommand_Click);
            // 
            // SendonTimerChk
            // 
            this.SendonTimerChk.AutoSize = true;
            this.SendonTimerChk.Checked = true;
            this.SendonTimerChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SendonTimerChk.Location = new System.Drawing.Point(368, 94);
            this.SendonTimerChk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SendonTimerChk.Name = "SendonTimerChk";
            this.SendonTimerChk.Size = new System.Drawing.Size(147, 19);
            this.SendonTimerChk.TabIndex = 17;
            this.SendonTimerChk.Text = "Parse Weight On Timer";
            this.SendonTimerChk.UseVisualStyleBackColor = true;
            // 
            // ScaleTypeCmbo
            // 
            this.ScaleTypeCmbo.DisplayMember = "ScaleName";
            this.ScaleTypeCmbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScaleTypeCmbo.FormattingEnabled = true;
            this.ScaleTypeCmbo.Location = new System.Drawing.Point(96, 54);
            this.ScaleTypeCmbo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ScaleTypeCmbo.Name = "ScaleTypeCmbo";
            this.ScaleTypeCmbo.Size = new System.Drawing.Size(156, 23);
            this.ScaleTypeCmbo.TabIndex = 19;
            this.ScaleTypeCmbo.SelectedIndexChanged += new System.EventHandler(this.ScaleTypeCmbo_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(14, 58);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(64, 15);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "Scale Type:";
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(96, 147);
            this.cbxParity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(156, 23);
            this.cbxParity.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(38, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Parity:";
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(198, 176);
            this.cbxStopBits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(54, 23);
            this.cbxStopBits.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(137, 180);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "Stop Bits:";
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxDataBits.Location = new System.Drawing.Point(96, 176);
            this.cbxDataBits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(35, 23);
            this.cbxDataBits.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(22, 179);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "Data Bits:";
            // 
            // chkDisplayLogOutput
            // 
            this.chkDisplayLogOutput.AutoSize = true;
            this.chkDisplayLogOutput.Location = new System.Drawing.Point(368, 118);
            this.chkDisplayLogOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkDisplayLogOutput.Name = "chkDisplayLogOutput";
            this.chkDisplayLogOutput.Size = new System.Drawing.Size(128, 19);
            this.chkDisplayLogOutput.TabIndex = 26;
            this.chkDisplayLogOutput.Text = "Display Log Output";
            this.chkDisplayLogOutput.UseVisualStyleBackColor = true;
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(696, 497);
            this.Controls.Add(this.chkDisplayLogOutput);
            this.Controls.Add(this.cbxDataBits);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbxStopBits);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxParity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ScaleTypeCmbo);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.SendonTimerChk);
            this.Controls.Add(this.btnSendManualCommand);
            this.Controls.Add(this.TexttosendTxt);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.BaudTxt);
            this.Controls.Add(this.COMPORTCmbo);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Label2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainInterface";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scale Integration App";
            this.notifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Timer HalfSecondTimer;
        private TextBox TextBox1;
        private ComboBox COMPORTCmbo;
        private Timer FiveSecondTimer;
        private TextBox BaudTxt;
        private Label Label2;
        private Label Label3;
        private Label Label5;
        private NotifyIcon NotifyIcon1;
        private Label TextBox2;
        private TextBox TexttosendTxt;
        private Button btnSendManualCommand;
        private CheckBox SendonTimerChk;
        private ComboBox ScaleTypeCmbo;
        private Label Label1;
        private ComboBox cbxParity;
        private Label label4;
        private ComboBox cbxStopBits;
        private Label label7;
        private ComboBox cbxDataBits;
        private Label label8;
        private CheckBox chkDisplayLogOutput;
        private ContextMenuStrip notifyIconContextMenu;
        private ToolStripMenuItem notifyIconBtnExit;
    }
}