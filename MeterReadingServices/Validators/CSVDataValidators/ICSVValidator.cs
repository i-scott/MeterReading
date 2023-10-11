namespace MeterReadingServices.Validators.CSVDataValidators
{
    public interface ICSVValidator
    {
        bool IsValid(string value);
    }
}