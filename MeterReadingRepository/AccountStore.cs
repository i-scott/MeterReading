using System;
using System.Threading.Tasks;
using Dapper;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using MeterReadingRepository.Dapper;
using Microsoft.Extensions.Logging;


namespace MeterReadingRepository
{
    public class AccountStore : IFetchData<Account, long>
    {
        private readonly DapperDBContext _dbContext;
        private readonly ILogger<AccountStore> _logger;

        public AccountStore(DapperDBContext dbContext, ILogger<AccountStore> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Account> FetchDataAsync(long key)
        {
            try
            {
                string fetchQuery = "select * from Accounts where AccountId = @AccountId";

                using (var connection = _dbContext.CreateConnection())
                {
                    var parameters = new { AccountId = key };
                    var result = await connection.QuerySingleOrDefaultAsync<Account>(fetchQuery, parameters);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to fetch account with ID {key}");
            }

            return null;
        }
    }
}
