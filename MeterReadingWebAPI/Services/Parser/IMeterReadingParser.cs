using MeterReadingWebAPI.Model;

namespace MeterReadingWebAPI.Services.Parser
{
    public interface IMeterReadingParser
    {
        public MeterReading ToMeterReading(string[] strings);
    }
}