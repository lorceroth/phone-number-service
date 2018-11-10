using Api.Formatters;
using Api.Models;
using Api.Validators;
using System;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers
{
    public class NumberController : ApiController
    {
        private NumberFormatter _formatter;

        private NumberValidator _validator;

        public NumberController(NumberFormatter formatter, NumberValidator validator)
        {
            _formatter = formatter;
            _validator = validator;
        }

        [HttpPost]
        [Route("format")]
        public IHttpActionResult Format(NumberDTO numberDTO)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest(GetModelErrorMessage());
            }

            var number = numberDTO.Number;

            try
            {
                var formattedNumber = _formatter.Format(number);

                return Ok(new NumberDTO(formattedNumber));
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("validate")]
        public IHttpActionResult Validate(NumberDTO numberDTO)
        {
            if (false == ModelState.IsValid)
            {
                return BadRequest(GetModelErrorMessage());
            }

            var number = numberDTO.Number;
            var valid = _validator.Validate(number);

            return Ok(new ValidationResultDTO(valid));
        }

        private string GetModelErrorMessage()
        {
            var error = ModelState
                .SelectMany(m => m.Value.Errors)
                .FirstOrDefault();

            return null != error ? error.ErrorMessage : null;
        }
    }
}
