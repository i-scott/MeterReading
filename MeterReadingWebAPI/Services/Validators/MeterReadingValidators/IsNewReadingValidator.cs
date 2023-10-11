using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services.Validators.MeterReadingValidators
{
    public class IsNewReadingValidator : IValidator
    {
        public bool IsValid(MeterReading reading) { return true; }
    }
}
