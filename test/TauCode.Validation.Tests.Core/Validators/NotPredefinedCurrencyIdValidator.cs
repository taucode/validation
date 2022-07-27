using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation.Tests.Core.Validators
{
    public class NotPredefinedCurrencyIdValidator<T> : PropertyValidator<T, long>
    {
        public override bool IsValid(ValidationContext<T> context, long value)
        {
            return !DataConstants.Currency.IsPredefinedCurrencyId(value);
        }

        public override string Name => "NotPredefinedCurrencyIdValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "'{PropertyName}' must not designate a pre-defined currency Id.";
        }
    }
}
