using System.Reflection;
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
            services.AddScoped<MeterReadingStore>();
        }
    }
}
