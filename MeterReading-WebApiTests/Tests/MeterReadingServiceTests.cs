using FluentAssertions;
using MeterReadingWebAPI.Model;
using MeterReadingWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace MeterReading_WebApiTests.Tests
{
    public class MeterReadingServiceTests
    {
        [Fact]                
        public void WhenGivenListOfFiles_ReturnsNumberProcessed()
        {
            var meterReadingValidator = new Mock<IMeterReadingParser>();
            meterReadingValidator.Setup(mr => mr.ToMeterReading(It.IsAny<string[]>())).Returns(new MeterReading());
            var sut = new MeterReadingService(meterReadingValidator.Object);

            var formFileCollection = new FormFileCollection();

            var content = "Hello world";
            var fileName = "test.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            formFileCollection.Add(new FormFile(stream, 0, stream.Length, "file", fileName));

            var result = sut.Process(formFileCollection);

            result.Should().Be(1);
        }

        [Fact]
        public void ShouldValidateCSVFile()
        {
            var meterReadingValidator = new Mock<IMeterReadingParser>();

            meterReadingValidator.Setup(mr => mr.ToMeterReading(It.IsAny<string[]>())).Returns(new MeterReading());

            var sut = new MeterReadingService(meterReadingValidator.Object);

            var formFileCollection = new FormFileCollection();

            var content = "AccountId,MeterReadingDateTime,MeterReadValue,\r\n2344,22/04/2019 09:24,1002,";
            var fileName = "test.csv";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            formFileCollection.Add(new FormFile(stream, 0, stream.Length, "file", fileName));

            var result = sut.Process(formFileCollection);

            meterReadingValidator.Verify(mr => mr.ToMeterReading(It.IsAny<string[]>()), Times.AtLeastOnce);
        }

    }
}
