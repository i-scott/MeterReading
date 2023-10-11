using FluentAssertions;
using MeterReadingServices.Validators.CSVDataValidators;
using Xunit;

namespace MeterReading_WebApiTests.Tests.ValidatorTests.CSVDataValidators
{
    public class MeterReadingDateValidatorTests
    {
        [Fact]
        public void WhenValueIsDate_ReturnsTrue()
        {
            var sut = new CSVMeterReadingDataValidator(DateTime.Now.AddYears(-1));

            var result = sut.IsValid(DateTime.Now.ToString());

            result.Should().BeTrue();
        }

        [Fact]
        public void WhenValueIsBeforeMinimumReadData_ReturnsFalse()
        {
            var sut = new CSVMeterReadingDataValidator(DateTime.Now.AddYears(-1));

            var result = sut.IsValid(DateTime.Now.AddYears(-2).ToString());

            result.Should().BeFalse();
        }

        [Fact]
        public void WhenValueIsNotDate_ReturnsFalse()
        {
            var sut = new CSVMeterReadingDataValidator(DateTime.Now.AddYears(-1));

            var result = sut.IsValid("bob date");

            result.Should().BeFalse();
        }

    }
}
