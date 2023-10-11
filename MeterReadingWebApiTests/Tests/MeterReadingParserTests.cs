using FluentAssertions;
using MeterReadingServices.Parser;
using Xunit;

namespace MeterReading_WebApiTests.Tests
{
    public class MeterReadingParserTests
    {
        [Fact]
        public void MeterReadinParserReturnsPopulatedMeterReading() 
        {
            var sut = new MeterReadingParser();

            string[] testdata = new string [] { "12345", "22/04/2019 09:24", "3333" };

            var result = sut.ToMeterReading(testdata);

            result.MeterReadValue.Should().Be(3333);
            result.AccountId.Should().Be(12345);
            result.MeterReadingDateTime.Should().Be(DateTime.Parse("22/04/2019 09:24"));
        }
    }
}
