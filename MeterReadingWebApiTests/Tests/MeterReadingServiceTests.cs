using FluentAssertions;
using MeterReadingWebAPI.Model;
using MeterReadingWebAPI.Services;
using MeterReadingWebAPI.Services.Parser;
using MeterReadingWebAPI.Services.Validators.CSVDataValidators;
using Moq;
using Xunit;

namespace MeterReading_WebApiTests.Tests
{
    public class MeterReadingServiceTests
    {
        private readonly string ApplicationBaseDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        [Fact]                
        public void WhenGivenValidFile_ReturnsNumberProcessed()
        {
            var meterReadingValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();

            meterReadingValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(true);
            meterReadingParser.Setup(mr => mr.ToMeterReading(It.IsAny<string[]>())).Returns(new MeterReading());

            var sut = new MeterReadingService(meterReadingValidator.Object, meterReadingParser.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\Single_Reading.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = sut.ImportFromFiles(files);

            result.Should().Be(1);
        }

        [Fact]
        public void WhenGivenInvalidFile_ReturnsZero()
        {
            var meterReadingValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();

            meterReadingValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(false);

            var sut = new MeterReadingService(meterReadingValidator.Object, meterReadingParser.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\Single_Reading.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = sut.ImportFromFiles(files);

            result.Should().Be(0);
        }
    }
}
