namespace TauCode.Validation
{
    public class CurrencyCodeValidator : CodeValidator
    {
        public CurrencyCodeValidator()
            : this("'{PropertyName}' must be a valid currency code.")
        {
        }

        public CurrencyCodeValidator(string message)
            : base(
                3,
                3,
                ValidationExtensions.CapitalLatinLetters,
                ValidationExtensions.CapitalLatinLetters,
                ValidationExtensions.EmptyChars,
                message)
        {
        }
    }
}
