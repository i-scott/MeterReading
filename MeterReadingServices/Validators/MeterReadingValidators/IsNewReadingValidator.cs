using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class IsNewReadingValidator : IValidator
    {
        private readonly IFetchData<MeterReading, long> _fetchMeterReadingData;
        private readonly ILogger<IsNewReadingValidator> _logger;

        public IsNewReadingValidator(IFetchData<MeterReading, long> fetchMeterReadingData, ILogger<IsNewReadingValidator> logger)
        {
            _fetchMeterReadingData = fetchMeterReadingData;
            _logger = logger;
        }

        public bool IsValid(MeterReading reading)
        {
            try
            {
                // dont like this would prefer to use ICriteria Object
                var criteria = $"SELECT * FROM MeterReadings where MeterReadingDateTime > @MeterReadingDateTime AND AccountId = @AccountId AND MeterReadingValue = @MeterReadingValue";

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
