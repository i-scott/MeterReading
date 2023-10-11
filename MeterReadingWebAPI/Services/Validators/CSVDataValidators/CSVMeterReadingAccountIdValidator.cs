namespace MeterReadingWebAPI.Services.Validators.CSVDataValidators
{
    public class CSVMeterReadingAccountIdValidator : ICSVValidator
    {
        public bool IsValid(string value)
        {
            if (!long.TryParse(value, out long result)) return false;

            if (result <= 0) return false;

            return true;
        }
    }
}
