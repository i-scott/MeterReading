using System;
using System.Threading.Tasks;
using CSVFile;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using Microsoft.Extensions.Logging;

namespace MeterReadingServices
{
    public class MeterReadingImportService : IMeterReadingImportService
    {
        private IMeterReadingParser _meterReadingParser;
        private readonly ILogger<MeterReadingImportService> _logger;
        private IMeterReadingCSVValidator _meterReadingCSVValidator;

        public MeterReadingImportService(IMeterReadingCSVValidator meterReadingValidator, IMeterReadingParser meterReadingParser, ILogger<MeterReadingImportService> logger)
        {
            _meterReadingParser = meterReadingParser;
            _logger = logger;
            _meterReadingCSVValidator = meterReadingValidator;
        }

        public async Task<int> ImportFromFilesAsync(string[] fileNames)
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
                            _logger.LogError(ex, $"Unable to import line {line} from {fileName}");
                        }
                    }
                }
            }
            return linesProcessed;
        }
    }
}
