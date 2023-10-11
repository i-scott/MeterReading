using MeterReadingModel;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class ReadingUniqueValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
