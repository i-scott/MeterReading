using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services.Validators.MeterReadingValidators
{
    public class ReadingUniqueValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
