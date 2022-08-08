namespace TauCode.Validation.Codes;

public class CurrencyCodeValidator<T> : CodeValidator<T>
{
    public CurrencyCodeValidator()
        : base(
            3,
            3,
            ValidationExtensions.CapitalLatinLetters,
            ValidationExtensions.CapitalLatinLetters,
            ValidationExtensions.EmptyChars)
    {
    }

    public override string Name => "CurrencyCodeValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}' must be a valid currency code.";
    }
}