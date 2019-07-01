namespace TauCode.Validation
{
    public class LowercaseCodeValidator : CodeValidator
    {
        public LowercaseCodeValidator(
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

        public LowercaseCodeValidator(
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool digitsAllowed = true)
            : this(
                minLength,
                maxLength,
                separator,
                digitsAllowed,
                "'{PropertyName}' must be a valid lowercase code.")
        {
        }

        private static char[] ResolveSeparators(char? separator)
        {
            if (!separator.HasValue)
            {
                return ValidationExtensions.EmptyChars;
            }

            switch (separator.Value)
            {
                case '_':
                    return ValidationExtensions.UnderscoreSeparator;

                case '-':
                    return ValidationExtensions.HyphenSeparator;

                default:
                    return new[] { separator.Value };
            }
        }

        private static char[] ResolveAlphabet(bool digitsAllowed)
        {
            return digitsAllowed
                ? ValidationExtensions.SmallLatinLettersAndDigits
                : ValidationExtensions.SmallLatinLetters;
        }
    }
}
