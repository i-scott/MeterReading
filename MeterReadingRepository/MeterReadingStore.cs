using MeterReadingModel;
using MeterReadingInterfaces.DataStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MeterReadingRepository
{
    public class MeterReadingStore : IStoreData<MeterReading, long>, IFetchData<MeterReading, long>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<MeterReadingStore> _logger;

        public MeterReadingStore(IApplicationDbContext dbContext, ILogger<MeterReadingStore> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<MeterReading?> FetchDataAsync(long key)
        {
            try
            {
                var result = await _dbContext.MeterReadings.SingleOrDefaultAsync(mr => mr.AccountId == key);
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
                var result = await _dbContext.MeterReadings.AddAsync(meterReading);

                var numberSaved = _dbContext.SaveChangesAsync(CancellationToken.None);

                return result.Entity.AccountId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Setting Meter reading for AccountId {meterReading.AccountId} for date {meterReading.MeterReadingDateTime}");
            }

            return null;
        }
    }
}
