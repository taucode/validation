using FluentValidation;
using FluentValidation.Validators;
using System;


namespace TauCode.Validation
{
    public class FullNameValidator<T> : PropertyValidator<T, string>
    {
        protected int MinLength { get; }
        protected int MaxLength { get; }

        public FullNameValidator(int minLength, int maxLength, bool canBeNull)
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

            this.CanBeNull = canBeNull;
        }

        public bool CanBeNull { get; }

        public override bool IsValid(ValidationContext<T> context, string value)
        {

            if (value == null)
            {
                return this.CanBeNull;
            }

            if (value.Length == 0)
            {
                return false;
            }

            if (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[^1]))
            {
                return false;
            }

            if (value.Length < this.MinLength || value.Length > this.MaxLength)
            {
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode) =>
            "'{PropertyName}' must be a valid full name.";

        public override string Name => "FullNameValidator";
    }
}
