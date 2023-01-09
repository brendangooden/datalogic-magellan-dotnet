namespace DataLogic.Magellan.Integration;

public class ScanSerialDataResponse : SerialDataResponse
{
    public override SerialDataResponseType ResponseType => SerialDataResponseType.Scan;
    /// <summary>
    /// The type of the barcode, if it is a successful scan event.
    /// </summary>
    public required BarcodeType BarcodeType { get; set; }
    /// <summary>
    /// Contains the barcode data if it is a successful scan event.
    /// </summary>
    public required string BarcodeData { get; set; }
}