namespace MeterReadingWebAPI.Services.Validators.CSVDataValidators
{
    public interface ICSVValidator
    {
        bool IsValid(string value);
    }
}