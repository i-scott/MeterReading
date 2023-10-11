using System;
using FluentAssertions;
using MeterReadingInterfaces.DataStore;
using MeterReadingModel;
using MeterReadingServices;
using MeterReadingServices.Parser;
using MeterReadingServices.Validators.CSVDataValidators;
using MeterReadingServices.Validators.MeterReadingValidators;
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
            var meterReadingCSVValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();
            var logger = new Mock<ILogger<MeterReadingImportService>>();

            var meterReadingValidator = new Mock<IMeterReadingValidator>();
            var meterReadingStore = new Mock<IStoreData<MeterReading, long>>();
                
            meterReadingCSVValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(true);
            meterReadingParser.Setup(mr => mr.ToMeterReading(It.IsAny<string[]>())).Returns(new MeterReading());

            meterReadingValidator.Setup(mr => mr.IsValid(It.IsAny<MeterReading>())).Returns(true);
            
            var sut = new MeterReadingImportService(meterReadingCSVValidator.Object, meterReadingParser.Object, meterReadingValidator.Object, meterReadingStore.Object, logger.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\Single_Reading.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = await sut.ImportFromFilesAsync(files);

            result.ProcessedSuccessfully.Should().Be(1);
            result.FilesProcessed.Should().Be(1);
        }

        [Fact]
        public async void WhenGivenInvalidFile_ReturnsZero()
        {
            var meterReadingCSVValidator = new Mock<IMeterReadingCSVValidator>();
            var meterReadingParser = new Mock<IMeterReadingParser>();
            var logger = new Mock<ILogger<MeterReadingImportService>>();

            var meterReadingValidator = new Mock<IMeterReadingValidator>();
            var meterReadingStore = new Mock<IStoreData<MeterReading, long>>();

            meterReadingCSVValidator.Setup(mr => mr.IsValid(It.IsAny<string[]>(), It.IsAny<string[]>())).Returns(false);

            var sut = new MeterReadingImportService(meterReadingCSVValidator.Object, meterReadingParser.Object, meterReadingValidator.Object, meterReadingStore.Object, logger.Object);

            var files = new[] { $"{ApplicationBaseDir}\\TestData\\DoesNotExist.csv" };
            // dont like this CSV FIle is Hard Value, should be abstracted out
            var result = await sut.ImportFromFilesAsync(files);

            result.FailedToProcess.Should().Be(0);
            result.FilesNotProcessed.Should().Be(1);
        }
    }
}
