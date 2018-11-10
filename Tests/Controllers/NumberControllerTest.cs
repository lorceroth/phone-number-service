using Api.Controllers;
using Api.Formatters;
using Api.Models;
using Api.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Tests.Controllers
{
    [TestClass]
    public class NumberControllerTest
    {
        [TestMethod]
        public void Format_ValidNumber_ReturnNumberDTO()
        {
            // Arrange
            var controller = CreateController();
            var numberDTO = new NumberDTO("+46712321324");
            var expectedNumber = "+46 712 32 13 24";

            // Act
            ValidateModel(numberDTO, controller);
            var response = controller.Format(numberDTO);

            // Assert
            response.Should()
                .BeOfType<OkNegotiatedContentResult<NumberDTO>>();

            (response as OkNegotiatedContentResult<NumberDTO>)
                .Content
                .Number
                .Should()
                .Be(expectedNumber);
        }

        [TestMethod]
        public void Format_InvalidCharacters_ReturnBadRequest()
        {
            // Arrange
            var controller = CreateController();
            var numberDTO = new NumberDTO("+467ABC21324");

            // Act
            ValidateModel(numberDTO, controller);
            var response = controller.Format(numberDTO);

            // Assert
            response.Should()
                .BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Validate_ValidNumber_ReturnValidationResultDTO()
        {
            // Arrange
            var controller = CreateController();
            var numberDTO = new NumberDTO("+46712312123");

            // Act
            ValidateModel(numberDTO, controller);
            var response = controller.Validate(numberDTO);

            // Assert
            response.Should()
                .BeOfType<OkNegotiatedContentResult<ValidationResultDTO>>();

            (response as OkNegotiatedContentResult<ValidationResultDTO>)
                .Content
                .Valid
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Validate_InvalidNumber_ReturnValidationResultDTO()
        {
            // Arrange
            var controller = CreateController();
            var numberDTO = new NumberDTO("00467562341424");

            // Act
            ValidateModel(numberDTO, controller);
            var response = controller.Validate(numberDTO);

            // Assert
            response.Should()
                .BeOfType<OkNegotiatedContentResult<ValidationResultDTO>>();

            (response as OkNegotiatedContentResult<ValidationResultDTO>)
                .Content
                .Valid
                .Should()
                .BeFalse();
        }

        [TestMethod]
        public void Validate_InvalidCharacters_ReturnBadRequest()
        {
            // Arrange
            var controller = CreateController();
            var numberDTO = new NumberDTO("T00467562341424");

            // Act
            ValidateModel(numberDTO, controller);
            var response = controller.Validate(numberDTO);

            // Assert
            response.Should()
                .BeOfType<BadRequestErrorMessageResult>();
        }

        private NumberController CreateController()
        {
            var validator = new NumberValidator();
            var formatter = new NumberFormatter(validator);
            var controller = new NumberController(formatter, validator);

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            return controller;
        }

        private void ValidateModel<T>(T model, ApiController controller)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, context, result, true);

            if (false == valid)
            {
                foreach (var item in result)
                {
                    controller
                        .ModelState
                        .AddModelError(item.MemberNames.First(), item.ErrorMessage);
                }
            }
        }
    }
}
