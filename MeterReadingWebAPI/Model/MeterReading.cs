using System;

namespace MeterReadingWebAPI.Model
{
    public class MeterReading
    {
        public long AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }

        public long MeterReadValue { get; set; }

    }
}
