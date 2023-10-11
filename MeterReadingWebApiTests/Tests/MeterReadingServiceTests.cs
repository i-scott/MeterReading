using System;
using FluentAssertions;
using MeterReadingModel;
using MeterReadingServices;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MeterReading_WebApiTests.Tests
{
    public class MeterReadingServiceTests
    {
        private readonly string ApplicationBaseDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        [Fact]                
        public async void WhenGivenValidFile_ReturnsNumberProcessed()
        {
            var meterReadingValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();
            var logger = new Mock<ILogger<MeterReadingImportService>>();

            meterReadingValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(true);
            meterReadingParser.Setup(mr => mr.ToMeterReading(It.IsAny<string[]>())).Returns(new MeterReading());

            var sut = new MeterReadingImportService(meterReadingValidator.Object, meterReadingParser.Object, logger.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\Single_Reading.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = await sut.ImportFromFilesAsync(files);

            result.ProcessedSuccessfully.Should().Be(1);
        }

        [Fact]
        public async void WhenGivenInvalidFile_ReturnsZero()
        {
            var meterReadingValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();
            var logger = new Mock<ILogger<MeterReadingImportService>>();

            meterReadingValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(false);

            var sut = new MeterReadingImportService(meterReadingValidator.Object, meterReadingParser.Object, logger.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\Single_Reading.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = await sut.ImportFromFilesAsync(files);

            result.FailedToProcess.Should().Be(0);
        }
    }
}
