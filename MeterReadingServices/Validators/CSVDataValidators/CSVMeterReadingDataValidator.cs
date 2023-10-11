using System;

namespace MeterReadingServices.Validators.CSVDataValidators
{
    public class CSVMeterReadingDataValidator : ICSVValidator
    {
        private readonly DateTime _minimumReadingDate;

        public CSVMeterReadingDataValidator(DateTime minimumReadingDate)
        {
            _minimumReadingDate = minimumReadingDate;
        }

        public bool IsValid(string value)
        {
            if (!DateTime.TryParse(value, out DateTime parsedDate)) return false;

            if (parsedDate < _minimumReadingDate) return false;

            return true;
        }
    }
}
