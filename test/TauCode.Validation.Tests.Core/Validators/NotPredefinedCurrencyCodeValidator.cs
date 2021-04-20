using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation.Tests.Core.Validators
{
    public class NotPredefinedCurrencyCodeValidator<T> : PropertyValidator<T, string>
    {
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "'{PropertyName}' must not designate a pre-defined currency code.";
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            return !DataConstants.Currency.IsPredefinedCurrencyCode(value);
        }

        public override string Name => "NotPredefinedCurrencyCodeValidator";


    }
}
