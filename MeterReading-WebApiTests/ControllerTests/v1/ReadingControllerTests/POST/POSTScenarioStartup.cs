using MeterReading_WebApiTests.ControllerTests.Scaffolding;
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
        }
    }
}
