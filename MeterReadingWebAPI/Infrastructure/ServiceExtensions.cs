using System;
using System.Collections.Generic;
using FluentMigrator.Runner;
using System.Reflection;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using MeterReadingServices;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using MeterReadingServices.Validators.MeterReadingValidators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MeterReadingWebAPI.AppServices;
using System.IO;
using Microsoft.AspNetCore.Mvc.Versioning;
using MeterReadingRepositoryMigrations;

namespace MeterReadingWebAPI.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            var validators = new Dictionary<string, ICSVValidator>
            {
                { "AccountId", new CSVMeterReadingAccountIdValidator() },
                { "MeterReadingDateTime", new CSVMeterReadingDataValidator(DateTime.Now.AddYears(-30)) },
                { "MeterReadValue", new CSVMeterReadingValidator() }
            };
            services.AddSingleton<IDictionary<string, ICSVValidator>>(validators);

            services.AddSingleton<IMeterReadingCSVValidator, MeterReadingCSVDataValidator>();
                
            services.AddSingleton<IMeterReadingValidator>((provider) =>
            {
                var accountFetch = provider.GetRequiredService<IFetchData<Account, long>>();
                var meterReadingFetch = provider.GetRequiredService<IFetchData<MeterReading, long>>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                var validators = new IValidator[]
                {
                    new ExistingAccountIdValidator(accountFetch, loggerFactory.CreateLogger<ExistingAccountIdValidator>()),
                    new IsNewReadingValidator(meterReadingFetch, loggerFactory.CreateLogger<IsNewReadingValidator>()),
                    new ReadingUniqueValidator(meterReadingFetch, loggerFactory.CreateLogger<ReadingUniqueValidator>())
                };

                return new MeterReadingValidator(validators);
            });

            services.AddSingleton<IMeterReadingImportService, MeterReadingImportService>();
            services.AddSingleton<IMeterReadingParser, MeterReadingParser>();
        }

        public static void AddMigrationsLogging( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2012()
                    .WithGlobalConnectionString(configuration.GetConnectionString("SqlConnection"))
                    .ScanIn(Assembly.GetAssembly(typeof(MigrationManager))).For.Migrations());
        }

        public static void AddTemporaryFileUploader(this IServiceCollection services) 
        {
            services.AddSingleton<IFormFileUploader>((provider) =>
            {
                var applicationDirectory = Directory.GetCurrentDirectory();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger<FormFileToTempStoreUploader>();
                return new FormFileToTempStoreUploader(applicationDirectory, logger);
            });
        }
    }
}
