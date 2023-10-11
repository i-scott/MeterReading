using FluentAssertions;
using MeterReadingWebAPI.Services.Validators.CSVDataValidators;
using Xunit;

namespace MeterReading_WebApiTests.Tests
{
    public class MeterReadingAccountIdValidatorTests
    {
        [Fact]
        public void WhenValueIsNumber_ReturnsTrue()
        {
            var sut = new CSVMeterReadingAccountIdValidator();

            var result = sut.IsValid("1234");

            result.Should().BeTrue();
        }

        [Fact]
        public void WhenValueIsZero_ReturnsFalse()
        {
            var sut = new CSVMeterReadingAccountIdValidator();

            var result = sut.IsValid("0");

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsBlank_ReturnsFalse()
        {
            var sut = new CSVMeterReadingAccountIdValidator();

            var result = sut.IsValid("");

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsNotNumeric_ReturnsFalse()
        {
            var sut = new CSVMeterReadingAccountIdValidator();

            var result = sut.IsValid("bob");

            result.Should().BeFalse();
        }

    }
}
