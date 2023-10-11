using System;
using System.Collections.Generic;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using MeterReadingServices;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using MeterReadingServices.Validators.MeterReadingValidators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MeterReadingWebAPI.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            var validators = new Dictionary<string, ICSVValidator>
            {
                { "AccountId", new CSVMeterReadingAccountIdValidator() },
                { "MeterReadingDateTime", new CSVMeterReadingDataValidator(DateTime.Now.AddYears(-30)) },
                { "MeterReadValue", new CSVMeterReadingValidator() }
            };

            services.AddSingleton<IDictionary<string, ICSVValidator>>(validators);

            services.AddSingleton<IMeterReadingImportService, MeterReadingImportService>();
            services.AddSingleton<IMeterReadingParser, MeterReadingParser>();
            services.AddSingleton<IMeterReadingCSVValidator, MeterReadingCSVDataValidator>();
                
            services.AddSingleton<IMeterReadingValidator>((provider) =>
            {
                var accountFetch = provider.GetService<IFetchData<Account, long>>();
                var meterReadingFetch = provider.GetService<IFetchData<MeterReading, long>>();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                var validators = new IValidator[]
                {
                    new ExistingAccountIdValidator(accountFetch, loggerFactory.CreateLogger<ExistingAccountIdValidator>()),
                    new IsNewReadingValidator(meterReadingFetch, loggerFactory.CreateLogger<IsNewReadingValidator>()),
                    new ReadingUniqueValidator(meterReadingFetch, loggerFactory.CreateLogger<ReadingUniqueValidator>())
                };

                return new MeterReadingValidator(validators);
            });
        }
    }
}
