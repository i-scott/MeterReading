using MeterReadingModel;

namespace MeterReadingServices.Parser
{
    public interface IMeterReadingParser
    {
        public MeterReading ToMeterReading(string[] strings);
    }
}