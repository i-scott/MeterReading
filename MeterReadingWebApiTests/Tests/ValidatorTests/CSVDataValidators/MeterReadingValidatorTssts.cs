using FluentAssertions;
using MeterReadingServices.Validators.CSVDataValidators;
using Xunit;

namespace MeterReading_WebApiTests.Tests.ValidatorTests.CSVDataValidators
{
    public class MeterReadingValidatorTests
    {
        [Fact]
        public void WhenValueIsNumber_ReturnsTrue()
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid("12345");

            result.Should().BeTrue();
        }

        [Fact]
        public void WhenValueIsDecimal_ReturnsFalse()
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid("123.45");

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsBlank_ReturnsFalse()
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid("");

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsSpace_ReturnsFalse()
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid(" ");

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsNegative_ReturnsFalse()
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid("-1");

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("VOID", false)]
        [InlineData("OX123", false)]
        [InlineData("123", true)]
        public void WhenValueIsNotNumeric_ReturnsFalse(string value, bool expectedResult)
        {
            var sut = new CSVMeterReadingValidator();

            var result = sut.IsValid(value);

            result.Should().Be(expectedResult);
        }
    }

}