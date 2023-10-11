using MeterReading_WebApiTests.ControllerTests.Scaffolding;
using MeterReadingServices;
using MeterReadingWebAPI.AppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;


namespace MeterReading_WebApiTests.ControllerTests.v1.ReadingControllerTests.POST
{
    public class POSTScenarioStartup<TControllerSource> : ScenarioStartup<TControllerSource>
    {
        public POSTScenarioStartup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            var mockMeterReadingService = new Mock<IMeterReadingImportService>();
            services.AddSingleton(mockMeterReadingService.Object);

            var mockFormFileUploaderMock = new Mock<IFormFileUploader>();
            services.AddSingleton(mockFormFileUploaderMock.Object);
        }
    }
}
