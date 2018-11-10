namespace Api.Models
{
    public class ValidationResultDTO
    {
        public ValidationResultDTO(bool valid) => Valid = valid;

        public bool Valid { get; set; }
    }
}