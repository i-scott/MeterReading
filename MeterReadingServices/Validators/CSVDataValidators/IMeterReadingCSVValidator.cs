namespace MeterReadingServices.Validators.CSVDataValidators
{
    public interface IMeterReadingCSVValidator
    {
        bool IsValid(string[] headers, string[] strings);
    }
}