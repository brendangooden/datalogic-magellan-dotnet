using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Magellan.Integration
{
    /// <summary>
    /// Represents the default settings used when initiating communication with a DataLogic scale via a COM (Serial) port.
    /// </summary>
    public class DefaultSerialPortConfiguration
    {
        /// <summary>
        /// The COM Port name e.g. "COM1". This is a required property.
        /// </summary>
        public required string SerialPortName { get; init; }

        /// <summary>
        /// The Baud Rate for the connection. Defaults to 9600
        /// </summary>
        public int BaudRate { get; init; } = 9600;
        /// <summary>
        /// The Parity for the serial port. Defaults to <see cref="System.IO.Ports.Parity.None"/>
        /// </summary>
        public Parity Parity { get; init; } = Parity.None;
        /// <summary>
        /// The DataBits for the serial port. Defaults to 7.
        /// </summary>
        public int DataBits { get; init; } = 7;
        /// <summary>
        /// The StopBits for the serial port. Defaults to <see cref="System.IO.Ports.StopBits.One"/>
        /// </summary>
        public StopBits StopBits { get; init; } = StopBits.One;
    }
}
