using FluentMigrator.Runner;
using MeterReadingRepository.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MeterReadingRepositoryMigrations
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(MigrationManager));
            try
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<DapperDatabase>();

                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                databaseService.CreateDatabase("ensek");

                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unable to create/migrate database");
                throw;
            }

            return host;
        }
    }
}
