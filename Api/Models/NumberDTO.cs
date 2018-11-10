using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class NumberDTO
    {
        public NumberDTO(string number) => Number = number;

        [RegularExpression(@"[0-9\+]+", ErrorMessage = "The number contains invalid characters.")]
        public string Number { get; set; }
    }
}