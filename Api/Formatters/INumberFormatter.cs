namespace Api.Formatters
{
    /// <summary>
    /// Formats phone numbers according to Swedish standards.
    /// </summary>
    public interface INumberFormatter
    {
        /// <summary>
        /// Formats a phone number.
        /// </summary>
        /// <param name="number">The number to format.</param>
        /// <returns></returns>
        /// <exception cref="System.FormatException">
        /// Thrown when the number is invalid according to the <see cref="Api.Validators.INumberValidator">INumberValidator</see>.
        /// </exception>
        string Format(string number);
    }
}
