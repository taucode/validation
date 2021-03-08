namespace TauCode.Validation
{
    public class UppercaseCodeValidator : CodeValidator
    {
        public UppercaseCodeValidator(
            int minLength,
            int maxLength,
            char? separator,
            bool digitsAllowed,
            string message)
            : base(
                minLength,
                maxLength,
                ResolveAlphabet(digitsAllowed),
                ResolveAlphabet(digitsAllowed),
                ResolveSeparators(separator),
                message)
        {
        }

        public UppercaseCodeValidator(
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool digitsAllowed = true)
            : this(
                minLength,
                maxLength,
                separator,
                digitsAllowed,
                "'{PropertyName}' must be a valid uppercase code.")
        {
        }

        private static char[] ResolveSeparators(char? separator)
        {
            if (!separator.HasValue)
            {
                return ValidationExtensions.EmptyChars;
            }

            return separator.Value switch
            {
                '_' => ValidationExtensions.UnderscoreSeparator,
                '-' => ValidationExtensions.HyphenSeparator,
                _ => new[] {separator.Value}
            };
        }

        private static char[] ResolveAlphabet(bool digitsAllowed)
        {
            return digitsAllowed
                ? ValidationExtensions.CapitalLatinLettersAndDigits
                : ValidationExtensions.CapitalLatinLetters;
        }
    }
}
