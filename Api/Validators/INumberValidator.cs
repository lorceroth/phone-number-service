namespace Api.Validators
{
    /// <summary>
    /// Validates phone numbers according to Swedish standards.
    /// </summary>
    public interface INumberValidator
    {
        /// <summary>
        /// The default error message.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Validates a phone number.
        /// </summary>
        /// <param name="number">The number to validate.</param>
        /// <returns></returns>
        bool Validate(string number);
    }
}
