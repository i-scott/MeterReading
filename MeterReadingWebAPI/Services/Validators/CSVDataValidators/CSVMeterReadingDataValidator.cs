using System;

namespace MeterReadingWebAPI.Services.Validators.CSVDataValidators
{
    public class MeterReadingDataValidator : IValidator
    {
        private readonly DateTime _minimumReadingDate;

        public MeterReadingDataValidator(DateTime minimumReadingDate)
        {
            _minimumReadingDate = minimumReadingDate;
        }

        public bool IsValid(string value)
        {            
            if(!DateTime.TryParse(value, out DateTime parsedDate)) return false;            

            if(parsedDate < _minimumReadingDate) return false;

            return true;
        }
    }
}
