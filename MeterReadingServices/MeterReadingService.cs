using CSVFile;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using System;

namespace MeterReadingServices
{
    public class MeterReadingService : IMeterReadingService
    {
        private IMeterReadingParser _meterReadingParser;
        private IMeterReadingCSVValidator _meterReadingCSVValidator;

        public MeterReadingService(IMeterReadingCSVValidator meterReadingValidator, IMeterReadingParser meterReadingParser)
        {
            _meterReadingParser = meterReadingParser;
            _meterReadingCSVValidator = meterReadingValidator;
        }

        public int ImportFromFiles(string[] fileNames)
        {
            int linesProcessed = 0;

            foreach (string fileName in fileNames)
            {
                // dont like this CSV FIle is Hard Value, should be abstracted out
                using (var csvReader = CSVReader.FromFile(fileName))
                {
                    foreach (var line in csvReader)
                    {
                        try
                        {
                            if (_meterReadingCSVValidator.IsValid(csvReader.Headers, line))
                            {
                                var meterReading = _meterReadingParser.ToMeterReading(line);
                                
                                linesProcessed++;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return linesProcessed;
        }
    }
}
