namespace TauCode.Validation.Codes;

public class LowercaseCodeValidator<T> : CodeValidator<T>
{
    public LowercaseCodeValidator(
        int minLength,
        int maxLength,
        char? separator,
        bool digitsAllowed)
        : base(
            minLength,
            maxLength,
            ResolveAlphabet(digitsAllowed),
            ResolveAlphabet(digitsAllowed),
            ResolveSeparators(separator))
    {
    }

    private static HashSet<char> ResolveSeparators(char? separator)
    {
        if (!separator.HasValue)
        {
            return ValidationExtensions.EmptyChars;
        }

        return separator.Value switch
        {
            '_' => ValidationExtensions.UnderscoreSeparator,
            '-' => ValidationExtensions.HyphenSeparator,
            _ => new[] { separator.Value }.ToHashSet(),
        };
    }

    private static HashSet<char> ResolveAlphabet(bool digitsAllowed)
    {
        return digitsAllowed
            ? ValidationExtensions.SmallLatinLettersAndDigits
            : ValidationExtensions.SmallLatinLetters;
    }
}