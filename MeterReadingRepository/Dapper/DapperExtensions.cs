using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using Microsoft.Extensions.DependencyInjection;

namespace MeterReadingRepository.Dapper
{
    public static class DapperExtensions
    {
        public static void AddDapper(this IServiceCollection services)
        {
            services.AddSingleton<DapperDBContext>();
            services.AddSingleton<DapperDatabase>();

            services.AddScoped<AccountStore>();
            services.AddSingleton<IFetchData<Account, long>>(p => p.GetRequiredService<AccountStore>());

            services.AddScoped<MeterReadingStore>();
            services.AddSingleton<IStoreData<MeterReading, long?>>(p => p.GetRequiredService<MeterReadingStore>());
            services.AddSingleton<IFetchData<MeterReading, long>>(x => x.GetRequiredService<MeterReadingStore>());
        }
    }
}
