namespace DataLogic.Magellan.Integration;

public class WeightSerialDataResponse : SerialDataResponse
{
    public override SerialDataResponseType ResponseType => SerialDataResponseType.Weight;
    /// <summary>
    /// Contains the weight data in Grams if its a successful weight event.
    /// </summary>
    public int? WeightGrams { get; set; }
    /// <summary>
    /// Weight in Kilograms, if required.
    /// </summary>
    public double? WeightKilograms => WeightGrams / (double)1000;
}