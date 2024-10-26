using FluentValidation;
using FluentValidation.Validators;
using TauCode.Validation.Codes;

namespace TauCode.Validation;

public static class ValidationExtensions
{
    internal static readonly HashSet<char> EmptyChars = new HashSet<char>();
    internal static readonly HashSet<char> CapitalLatinLetters;
    internal static readonly HashSet<char> CapitalLatinLettersAndDigits;
    internal static readonly HashSet<char> SmallLatinLetters;
    internal static readonly HashSet<char> SmallLatinLettersAndDigits;
    internal static readonly HashSet<char> LatinLetters;
    internal static readonly HashSet<char> LatinLettersAndDigits;
    internal static readonly HashSet<char> Digits;
    internal static readonly HashSet<char> HyphenSeparator = new HashSet<char>(new[] { '-' });
    internal static readonly HashSet<char> UnderscoreSeparator = new HashSet<char>(new[] { '_' });

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

        CapitalLatinLetters = capitalLatinLetters.ToHashSet();
        SmallLatinLetters = smallLatinLetters.ToHashSet();
        LatinLetters = latinLetters.ToHashSet();
        Digits = digits.ToHashSet();
        SmallLatinLettersAndDigits = smallLatinLettersAndDigits.ToHashSet();
        CapitalLatinLettersAndDigits = capitalLatinLettersAndDigits.ToHashSet();
        LatinLettersAndDigits = latinLettersAndDigits.ToHashSet();
    }

    public static IRuleBuilderOptions<T, string> CurrencyCode<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CurrencyCodeValidator<T>());
    }

    public static IRuleBuilderOptions<T, string> FullName<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength,
        int maxLength,
        bool canBeNull)
    {
        return ruleBuilder.SetValidator(new FullNameValidator<T>(
            minLength,
            maxLength,
            canBeNull));
    }

    public static IRuleBuilderOptions<T, TProperty?> WrapValueTypeValidator<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        PropertyValidator<T, TProperty> valueTypePropertyValidator)
        where TProperty : struct
    {
        return ruleBuilder.SetValidator(
            new ValueTypePropertyValidatorWrapper<T, TProperty>(valueTypePropertyValidator));
    }

    public static IRuleBuilderOptions<T, long> LongId<T>(
        this IRuleBuilder<T, long> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new LongIdValidator<T>());
    }

    public static IRuleBuilderOptions<T, short> ShortId<T>(
        this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new ShortIdValidator<T>());
    }

    public static IRuleBuilderOptions<T, int> IntId<T>(
        this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new IntIdValidator<T>());
    }
}