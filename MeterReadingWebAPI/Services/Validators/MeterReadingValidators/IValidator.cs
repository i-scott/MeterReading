using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services.Validators.MeterReadingValidators
{
    public interface IValidator
    {
        bool IsValid(MeterReading reading);
}
