using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TauCode.Validation
{
    public static class ValidationExtensions
    {
        internal static readonly char[] EmptyChars = { };
        internal static readonly char[] CapitalLatinLetters;
        internal static readonly char[] CapitalLatinLettersAndDigits;
        internal static readonly char[] SmallLatinLetters;
        internal static readonly char[] SmallLatinLettersAndDigits;
        internal static readonly char[] LatinLetters;
        internal static readonly char[] LatinLettersAndDigits;
        internal static readonly char[] Digits;
        internal static readonly char[] HyphenSeparator = { '-' };
        internal static readonly char[] UnderscoreSeparator = { '_' };

        static ValidationExtensions()
        {
            var capitalLatinLetters = Enumerable
                .Range('A', 'Z' - 'A' + 1)
                .Select(x => (char)x)
                .ToArray();

            var smallLatinLetters = Enumerable
                .Range('a', 'z' - 'a' + 1)
                .Select(x => (char)x)
                .ToArray();

            var digits = Enumerable
                .Range('0', '9' - '0' + 1)
                .Select(x => (char)x)
                .ToArray();

            var latinLetters = new List<char>();
            latinLetters.AddRange(capitalLatinLetters);
            latinLetters.AddRange(smallLatinLetters);

            var smallLatinLettersAndDigits = new List<char>();
            smallLatinLettersAndDigits.AddRange(smallLatinLetters);
            smallLatinLettersAndDigits.AddRange(digits);

            var capitalLatinLettersAndDigits = new List<char>();
            capitalLatinLettersAndDigits.AddRange(capitalLatinLetters);
            capitalLatinLettersAndDigits.AddRange(digits);

            var latinLettersAndDigits = new List<char>();
            latinLettersAndDigits.AddRange(latinLetters);
            latinLettersAndDigits.AddRange(digits);

            CapitalLatinLetters = capitalLatinLetters.ToArray();
            SmallLatinLetters = smallLatinLetters.ToArray();
            LatinLetters = latinLetters.ToArray();
            Digits = digits.ToArray();
            SmallLatinLettersAndDigits = smallLatinLettersAndDigits.ToArray();
            CapitalLatinLettersAndDigits = capitalLatinLettersAndDigits.ToArray();
            LatinLettersAndDigits = latinLettersAndDigits.ToArray();
        }

        public static IRuleBuilderOptions<T, string> Code<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength,
            int maxLength,
            char[] alphabet,
            char[] startingChars,
            char[] separators)
        {
            return ruleBuilder.SetValidator(new CodeValidator(
                minLength,
                maxLength,
                alphabet,
                startingChars,
                separators));
        }

        public static IRuleBuilderOptions<T, string> CurrencyCode<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CurrencyCodeValidator());
        }

        public static IRuleBuilderOptions<T, DateTime> ExactDate<T>(
            this IRuleBuilder<T, DateTime> ruleBuilder,
            DateTime? minDate = null,
            DateTime? maxDate = null)
        {
            return ruleBuilder.SetValidator(new ExactDateValidator(minDate, maxDate));
        }

        public static IRuleBuilderOptions<T, string> Username<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool lowercaseOnly = true,
            bool digitsAllowed = true)
        {
            return ruleBuilder.SetValidator(new UsernameValidator(
                minLength,
                maxLength,
                separator,
                lowercaseOnly,
                digitsAllowed));
        }

        public static IRuleBuilderOptions<T, string> FullName<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength,
            int maxLength)
        {
            return ruleBuilder.SetValidator(new FullNameValidator(
                minLength,
                maxLength));
        }

        public static IRuleBuilderOptions<T, string> WebCode<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength,
            int maxLength)
        {
            return ruleBuilder.SetValidator(new WebCodeValidator(
                minLength,
                maxLength));
        }

        public static IRuleBuilderOptions<T, string> LowercaseCode<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool digitsAllowed = true)
        {
            return ruleBuilder.SetValidator(new LowercaseCodeValidator(
                minLength,
                maxLength,
                separator,
                digitsAllowed));
        }

        public static IRuleBuilderOptions<T, string> UppercaseCode<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int minLength = 1,
            int maxLength = 100,
            char? separator = '_',
            bool digitsAllowed = true)
        {
            return ruleBuilder.SetValidator(new UppercaseCodeValidator(
                minLength,
                maxLength,
                separator,
                digitsAllowed));
        }
    }
}
