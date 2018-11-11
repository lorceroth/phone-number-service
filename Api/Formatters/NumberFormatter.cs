using Api.Validators;
using System;

namespace Api.Formatters
{
    public class NumberFormatter : INumberFormatter
    {
        private string _format = "{0} {1:### ## ## ##}";

        private INumberValidator _validator;

        public NumberFormatter(INumberValidator validator)
        {
            _validator = validator;
        }

        public string Format(string number)
        {
            if (false == _validator.Validate(number))
            {
                throw new FormatException(_validator.ErrorMessage);
            }

            var code = number.Substring(0, 3);
            var digits = int.Parse(number.Substring(3));

            return string.Format(_format, code, digits);
        }
    }
}