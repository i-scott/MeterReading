using System;
using System.Collections.Generic;
using MeterReadingServices;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}
