# DataLogic Magellan Integration for .NET
A .NET C# class library for communicating with a Datalogic Magellan 9xxx series Fixed Retail scanner/scales and a sample WinForms Desktop App.

This library has been written to handle communication with the scanner/scale via single RS-232 cable interface. Dual cable interface is available however I have not tested or implemented this.

Sample code for setting up the connection.

```
// Instantiate a new connection with the scale
var scaleConfig = new DefaultSerialPortConfiguration()
{
    SerialPortName = "COM1,       // Will be whatever COM port is attached to the scale. Usually via USB-Serial adapater.
    BaudRate = 9600,              // Default is 9600
    Parity = Parity.Odd,          // Default is Parity.Odd
    StopBits = StopBits.One,      // Default is StopBits.One
    DataBits = 7                  // Default is 7
};

// create the scale variable with the selected config, and optionally an ILogger for error logging.
var magellanScale = new SingleCableInterface(scaleConfig, _fileErrorLogger);

// Subscribe to the Scan/Weight data received events.
magellanScale.OnScanDataReceived += OnScanDataReceived;
magellanScale.OnWeightDataReceived += OnWeightDataReceived;

// Open the port and start listening.
magellanScale.OpenPort();
```

In all cases, barcode scans are sent to the host unsolicited but scale weight data must be requested from the scale by the host. 
```
await magellanScale.SendRequestWeightCommand();
```
Immediately after the `OnWeightDataReceived` event will fire, which can be handled and displayed to the user or used in an application.
