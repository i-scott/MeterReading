using System;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using Microsoft.Extensions.Logging;

namespace MeterReadingServices.Validators.MeterReadingValidators
{
    public class ExistingAccountIdValidator : IValidator
    {
        private readonly IFetchData<Account, long> _fetchAccountData;
        private readonly ILogger _logger;

        public ExistingAccountIdValidator(IFetchData<Account, long> fetchAccountData, ILogger logger)
        {
            _fetchAccountData = fetchAccountData;
            _logger = logger;
        }

        public bool IsValid(MeterReading reading)
        {
            try
            {
                var result = _fetchAccountData.FetchDataAsync(reading.AccountId);
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
