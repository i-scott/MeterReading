using MeterReadingModel;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public interface IValidator
    {
        bool IsValid(MeterReading reading);
    }
}
