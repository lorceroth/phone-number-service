using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Api.Validators;

namespace Tests.Validators
{
    [TestClass]
    public class NumberValidatorTest
    {
        [TestMethod]
        public void Validate_ValidNumber_ReturnTrue()
        {
            // Arrange
            var validator = new NumberValidator();
            var number = "+46712312123";

            // Act
            var validationResult = validator.Validate(number);

            // Assert
            validationResult.Should().BeTrue();
        }

        [TestMethod]
        public void Validate_TooManyDigits_ReturnFalse()
        {
            // Arrange
            var validator = new NumberValidator();
            var number = "00467562341424";

            // Act
            var validationResult = validator.Validate(number);

            // Assert
            validationResult.Should().BeFalse();
        }

        [TestMethod]
        public void Validate_InvalidCharacters_ReturnFalse()
        {
            // Arrange
            var validator = new NumberValidator();
            var number = "T00467562341424";

            // Act
            var validationResult = validator.Validate(number);

            // Assert
            validationResult.Should().BeFalse();
        }
    }
}
