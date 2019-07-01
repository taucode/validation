namespace TauCode.Validation
{
    public class UsernameValidator : CodeValidator
    {
        public UsernameValidator(
            int minLength,
            int maxLength,
            char? separator,
            bool lowercaseOnly,
            bool digitsAllowed,
            string message)
            : base(
                minLength,
                maxLength,
                ResolveAlphabet(lowercaseOnly, digitsAllowed),
                ResolveAlphabet(lowercaseOnly, digitsAllowed),
                ResolveSeparators(separator),
                message)
        {
        }

        public UsernameValidator(
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool lowercaseOnly = true,
            bool digitsAllowed = true)
            : this(
                minLength,
                maxLength,
                separator,
                lowercaseOnly,
                digitsAllowed,
                "'{PropertyName}' must be a valid username.")
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

        private static char[] ResolveAlphabet(bool lowercaseOnly, bool digitsAllowed)
        {
            if (lowercaseOnly)
            {
                if (digitsAllowed)
                {
                    return ValidationExtensions.SmallLatinLettersAndDigits;
                }
                else
                {
                    return ValidationExtensions.SmallLatinLetters;
                }
            }
            else
            {
                if (digitsAllowed)
                {
                    return ValidationExtensions.LatinLettersAndDigits;
                }
                else
                {
                    return ValidationExtensions.LatinLetters;
                }
            }
        }
    }
}
