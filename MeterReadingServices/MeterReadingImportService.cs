using System;
using System.Threading.Tasks;
using CSVFile;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using MeterReadingModel.View;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using MeterReadingServices.Validators.MeterReadingValidators;
using Microsoft.Extensions.Logging;

namespace MeterReadingServices
{
    public class MeterReadingImportService : IMeterReadingImportService
    {
        private IMeterReadingParser _meterReadingParser;
        private readonly IMeterReadingValidator _meterReadingValidator;
        private readonly IStoreData<MeterReading, long> _meterReadingStore;
        private readonly ILogger<MeterReadingImportService> _logger;
        private IMeterReadingCSVValidator _meterReadingCSVValidator;

        public MeterReadingImportService(IMeterReadingCSVValidator meterReadingCSVValidator, 
            IMeterReadingParser meterReadingParser, 
            IMeterReadingValidator meterReadingValidator,
            IStoreData<MeterReading, long> meterReadingStore,
            ILogger<MeterReadingImportService> logger)
        {
            _meterReadingParser = meterReadingParser;
            _meterReadingValidator = meterReadingValidator;
            _meterReadingStore = meterReadingStore;
            _logger = logger;
            _meterReadingCSVValidator = meterReadingCSVValidator;
        }

        public async Task<CSVImportProcessedResponse> ImportFromFilesAsync(string[] fileNames)
        {
            var processResponse = new CSVImportProcessedResponse();

            foreach (string fileName in fileNames)
            {
                try
                {
                    // dont like this CSV FIle is Hard Value, should be abstracted out
                    using (var csvReader = CSVReader.FromFile(fileName))
                    {
                        foreach (var line in csvReader)
                        {
                            try
                            {
                                if (!_meterReadingCSVValidator.IsValid(csvReader.Headers, line))
                                {
                                    processResponse.FailedToProcess++;
                                    continue;
                                }

                                var meterReading = _meterReadingParser.ToMeterReading(line);

                                if (!_meterReadingValidator.IsValid(meterReading))
                                {
                                    processResponse.FailedToProcess++;
                                    continue;
                                }

                                _meterReadingStore.SetAsync(meterReading);
                                processResponse.ProcessedSuccessfully++;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Unable to import line {line} from {fileName}");
                                processResponse.FailedToProcess++;
                            }
                        }

                        processResponse.FilesProcessed++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Unable to import from {fileName} does not exist");
                    processResponse.FilesNotProcessed++;
                }
            }
            return processResponse;
        }
    }
}
