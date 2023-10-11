using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeterReadingRepository.Dapper
{
    public static class DapperExtensions
    {
        public static void AddDapper(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DapperDBContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
