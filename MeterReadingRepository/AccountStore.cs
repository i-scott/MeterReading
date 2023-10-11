using System;
using System.Threading.Tasks;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MeterReadingRepository
{
    public class AccountStore : IFetchData<Account, long>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<MeterReadingStore> _logger;

        public AccountStore(IApplicationDbContext dbContext, ILogger<MeterReadingStore> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Account> FetchDataAsync(long key)
        {
            try
            {
                var result = await _dbContext.Accounts.SingleOrDefaultAsync(act => act.Id == key);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to fetch account with ID {key}");
            }

            return null;
        }
    }
}
