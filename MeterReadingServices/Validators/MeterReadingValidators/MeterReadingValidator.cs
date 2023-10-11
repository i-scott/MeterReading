using MeterReadingModel;
using Microsoft.Extensions.Logging;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class MeterReadingValidator : IMeterReadingValidator
    {
        private readonly IValidator[] _validators;

        public MeterReadingValidator(IValidator[] validators)
        {
            _validators = validators;
        }

        public bool IsValid(MeterReading reading)
        {
            foreach (var validator in _validators)
            {
                if( !validator.IsValid(reading)) return false;
            }

            return true;
        }
    }
}
