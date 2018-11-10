using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Api.Formatters;
using Api.Validators;

namespace Tests.Formatters
{
    [TestClass]
    public class NumberFormatterTest
    {
        [TestMethod]
        public void Format_ValidNumber_ReturnFormattedNumber()
        {
            // Arrange
            var formatter = CreateFormatter();
            var number = "+46712321324";
            var expectedNumber = "+46 712 32 13 24";

            // Act
            var formattedNumber = formatter.Format(number);

            // Assert
            formattedNumber.Should().Be(expectedNumber);
        }

        [TestMethod]
        public void Format_InvalidNumber_ThrowsFormatException()
        {
            // Arrange
            var formatter = CreateFormatter();
            var number = "+467ABC21324";

            // Act
            Action formatNumberAction = () => formatter.Format(number);

            // Assert
            formatNumberAction.Should()
                .Throw<FormatException>()
                .WithMessage("Invalid number format.");
        }

        private NumberFormatter CreateFormatter()
        {
            var validator = new NumberValidator();

            return new NumberFormatter(validator);
        }
    }
}
