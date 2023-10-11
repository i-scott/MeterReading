using MeterReadingModel;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class ExistingAccountIdValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
