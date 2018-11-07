using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Integration.Tests
{
    [TestClass]
    public class ApiTests
    {
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost/phonenumberservice/")
            };
        }

        [TestMethod]
        public async Task PhonenumberController_ValidateTrue_ReturnOk()
        {
            // Arrange
            var input = "+46712312123";

            // Act
            var result = await _client.PostAsync("validate", GetRequestObject(input));
            var json = await result.Content.ReadAsStringAsync();
            var validationResult = JsonConvert.DeserializeAnonymousType(json, new { valid = false });

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            validationResult.valid.Should().BeTrue();
        }

        [TestMethod]
        public async Task PhonenumberController_ValidateFalse_ReturnOk()
        {
            // Arrange
            var input = "00467562341424";

            // Act
            var result = await _client.PostAsync("validate", GetRequestObject(input));
            var json = await result.Content.ReadAsStringAsync();
            var validationResult = JsonConvert.DeserializeAnonymousType(json, new { valid = false });

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            validationResult.valid.Should().BeFalse();
        }

        [TestMethod]
        public async Task PhonenumberController_Validate_ReturnBadRequest()
        {
            // Arrange
            var input = "T00467562341424";

            // Act
            var result = await _client.PostAsync("validate", GetRequestObject(input));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task PhonenumberController_Format_ReturnOk()
        {
            // Arrange
            var input = "+46712321324";
            var expected = "+46 712 32 13 24";

            // Act
            var result = await _client.PostAsync("format", GetRequestObject(input));
            var json = await result.Content.ReadAsStringAsync();
            var formattedResult = JsonConvert.DeserializeAnonymousType(json, new { number = "" });

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            formattedResult.number.Should().Be(expected);
        }

        [TestMethod]
        public async Task PhonenumberController_Format_ReturnBadRequest()
        {
            // Arrange
            var input = "+467ABC21324";

            // Act
            var result = await _client.PostAsync("format", GetRequestObject(input));
            var json = await result.Content.ReadAsStringAsync();
            var formattedResult = JsonConvert.DeserializeAnonymousType(json, new { number = "" });

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private static StringContent GetRequestObject(string number)
        {
            return new StringContent(JsonConvert.SerializeObject(new { number = number }));
        }
    }
}
