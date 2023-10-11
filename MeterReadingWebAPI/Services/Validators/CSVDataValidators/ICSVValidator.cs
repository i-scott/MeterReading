namespace MeterReadingWebAPI.Services.Validators.CSVDataValidators
{
    public interface IValidator
    {
        bool IsValid(string value);
    }
}