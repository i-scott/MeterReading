using System;
using System.Threading;
using System.Threading.Tasks;
using MeterReadingModel;
using MeterReadingInterfaces.DataStore;
using Microsoft.Extensions.Logging;
using MeterReadingRepository.Dapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MeterReadingRepository
{
    public class MeterReadingStore : IStoreData<MeterReading, long>, IFetchData<MeterReading, long>
    {
        private readonly DapperDBContext _dbContext;
        private readonly ILogger<MeterReadingStore> _logger;

        public MeterReadingStore(DapperDBContext dbContext, ILogger<MeterReadingStore> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<MeterReading?> FetchDataAsync(long key)
        {
            try
            {
                string fetchQuery = "select * from MeterReadings where AccountId = @AccountId, MeterReadValue = @MeterReadValue";

                using var connection = _dbContext.CreateConnection();
                var parameters = new { AccountId = key };
                var result = await connection.QuerySingleOrDefaultAsync<MeterReading>(fetchQuery, parameters);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Fetching reading with Id {key}");
            }

            return null;
        }

        public async Task<long?> SetAsync(MeterReading meterReading)
        {
            try
            {
                var sql = "INSERT INTO MeterReading(AccountId, MeterReadingDateTime, MeterReadValue) VALUES (@AccountId, @MeterReadingDateTime, @MeterReadValue)";

                using var connection = _dbContext.CreateConnection();
                var result = await connection.ExecuteAsync(sql, meterReading);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Setting Meter reading for AccountId {meterReading.AccountId} for date {meterReading.MeterReadingDateTime}");
            }

            return null;
        }
    }
}
