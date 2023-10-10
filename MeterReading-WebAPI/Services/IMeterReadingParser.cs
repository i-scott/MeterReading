using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services
{
    public interface IMeterReadingParser
    {
        public MeterReading ToMeterReading(string [] strings);
    }
}