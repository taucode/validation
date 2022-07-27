using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation
{
    public class NullableNotEmptyValidator<T, TProperty> : NotEmptyValidator<T, TProperty?>
        where TProperty : struct
    {
        private readonly NotEmptyValidator<T, TProperty> _underlyingNotEmptyValidator;

        public NullableNotEmptyValidator()
        {
            _underlyingNotEmptyValidator = new NotEmptyValidator<T, TProperty>();
        }

        public override string Name => _underlyingNotEmptyValidator.Name;


        public override bool IsValid(ValidationContext<T> context, TProperty? value)
        {
            if (value.HasValue)
            {
                return _underlyingNotEmptyValidator.IsValid(context, value.Value);
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            IPropertyValidator casted = _underlyingNotEmptyValidator;
            return casted.GetDefaultMessageTemplate(errorCode);
        }
    }
}
