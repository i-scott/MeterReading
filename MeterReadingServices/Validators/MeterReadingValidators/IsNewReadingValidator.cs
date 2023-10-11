using MeterReadingModel;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class IsNewReadingValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
