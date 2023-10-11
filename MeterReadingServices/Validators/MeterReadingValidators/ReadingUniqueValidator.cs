using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using Microsoft.Extensions.Logging;
using System;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class ReadingUniqueValidator : IValidator
    {
        private readonly IFetchData<MeterReading, long> _fetchMeterReadingData;
        private readonly ILogger<ReadingUniqueValidator> _logger;

        public ReadingUniqueValidator(IFetchData<MeterReading, long> fetchMeterReadingData, ILogger<ReadingUniqueValidator> logger)
        {
            _fetchMeterReadingData = fetchMeterReadingData;
            _logger = logger;
        }

        public bool IsValid(MeterReading reading)
        {
            try
            {
                // dont like this would prefer to use ICriteria Object
                var criteria = $"SELECT * FROM MeterReadings where CAST( MeterReadingDateTime as DATE )> CAST(@MeterReadingDateTime AS DATE) AND AccountId = @AccountId AND MeterReadingValue = @MeterReadingValue";

                var parameters = new { MeterReadingDateTime = reading.MeterReadingDateTime, AccountId = reading.AccountId, MeterReadingValue = reading.MeterReadValue };

                var result = _fetchMeterReadingData.FetchDataAsync(criteria, parameters);

                if (result.Result != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Account with Id {reading.AccountId} can not be read");
            }

            return false;
        }
    }
}
