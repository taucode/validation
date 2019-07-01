using System.Collections.Generic;

namespace TauCode.Validation
{
    public class WebCodeValidator : CodeValidator
    {
        private static readonly char[] SmallLatinLettersAndDigits;

        static WebCodeValidator()
        {
            var list = new List<char>();
            list.AddRange(ValidationExtensions.SmallLatinLetters);
            list.AddRange(ValidationExtensions.Digits);

            SmallLatinLettersAndDigits = list.ToArray();
        }

        public WebCodeValidator(int minLength, int maxLength, string message)
            : base(
                minLength,
                maxLength,
                SmallLatinLettersAndDigits,
                SmallLatinLettersAndDigits,
                ValidationExtensions.HyphenSeparator,
                message)
        {
        }

        public WebCodeValidator(int minLength = 1, int maxLength = 100)
            : this(
                minLength,
                maxLength,
                "'{PropertyName}' must be a valid web code.")
        {
        }
    }
}
