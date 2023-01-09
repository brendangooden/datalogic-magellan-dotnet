namespace DataLogic.Magellan.Integration;

public abstract class SerialDataResponse
{
    /// <summary>
    /// The type of the response data.
    /// </summary>
    public abstract SerialDataResponseType ResponseType { get; }
    /// <summary>
    /// Whether the response was valid.
    /// </summary>
    public required bool IsValid { get; set; }
    /// <summary>
    /// Message (required if unsuccessful, optional if successful).
    /// </summary>
    public required string Message { get; set; }
    /// <summary>
    /// The raw string response of the serial data.
    /// </summary>
    public required string RawSerialResponse { get; set; }
}