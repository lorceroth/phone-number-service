using System;
using System.Text.RegularExpressions;

namespace Api.Validators
{
    public class NumberValidator
    {
        public static string ErrorMessage = "Invalid number format.";

        private string _format = @"^\+46[0-9]{9}$";

        private Regex _regex;

        public NumberValidator() => _regex = new Regex(_format);

        public bool Validate(string number)
        {
            return _regex.IsMatch(number);
        }
    }
}