using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation
{
    public class LongIdValidator<T> : PropertyValidator<T, long>
    {
        public override bool IsValid(ValidationContext<T> context, long value)
        {
            return value > 0L;
        }

        public override string Name => "LongIdValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "'{PropertyName}' must be a valid long Id.";
    }
}
