namespace Api.Validators
{
    public interface INumberValidator
    {
        string ErrorMessage { get; }

        bool Validate(string number);
    }
}
