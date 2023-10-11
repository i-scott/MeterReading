namespace MeterReadingWebAPI.Services.Validators.CSVDataValidators
{
    public class CSVMeterReadingValidator : ICSVValidator
    {
        public bool IsValid(string value)
        {
            if (!long.TryParse(value, out long result)) return false;

            if (result <= 0) return false;

            if (result > 99999) return false;

            return true;
        }
    }
}