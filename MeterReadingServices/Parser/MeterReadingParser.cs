using MeterReadingModel;
using System;

namespace MeterReadingServices.Parser
{
    public class MeterReadingParser : IMeterReadingParser
    {
        public MeterReading ToMeterReading(string[] strings)
        {
            var reading = new MeterReading();

            reading.AccountId = long.Parse(strings[0]);
            reading.MeterReadingDateTime = DateTime.Parse(strings[1]);
            reading.MeterReadValue = long.Parse(strings[2]);

            return reading;
        }
    }
}