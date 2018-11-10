using Api.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Formatters
{
    public class NumberFormatter
    {
        private string _format = "{0} {1:### ## ## ##}";

        private NumberValidator _validator;

        public NumberFormatter(NumberValidator validator)
        {
            _validator = validator;
        }

        public string Format(string number)
        {
            if (false == _validator.Validate(number))
            {
                throw new FormatException(NumberValidator.ErrorMessage);
            }

            var code = number.Substring(0, 3);
            var digits = int.Parse(number.Substring(3));

            return string.Format(_format, code, digits);
        }
    }
}