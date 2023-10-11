using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services.Validators.MeterReadingValidators
{
    public class ExistingAccountIdValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
