using FluentValidation.Validators;
using System;

namespace TauCode.Validation
{
    public class FullNameValidator : PropertyValidator
    {
        protected int MinLength { get; }
        protected int MaxLength { get; }

        public FullNameValidator(int minLength, int maxLength, string message)
            : base(message)
        {
            if (minLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength));
            }

            if (maxLength < minLength)
            {
                throw new ArgumentException($"'{nameof(maxLength)}' must be not less than '{nameof(minLength)}'", nameof(maxLength));
            }

            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }

        public FullNameValidator(int minLength, int maxLength)
            : this(minLength, maxLength, "'{PropertyName}' must be a valid full name.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var name = (string)context.PropertyValue;

            if (name == null)
            {
                return true; // not our case
            }

            if (name.Length == 0)
            {
                return false;
            }

            if (char.IsWhiteSpace(name[0]) || char.IsWhiteSpace(name[name.Length - 1]))
            {
                return false;
            }

            if (name.Length < this.MinLength || name.Length > this.MaxLength)
            {
                return false;
            }

            return true;

        }
    }
}
