using FluentValidation.Validators;
using System;

namespace TauCode.Validation
{
    public abstract class PropertyValidatorWithMessage : PropertyValidator
    {
        protected PropertyValidatorWithMessage(string message)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        protected string Message { get; }

        protected override string GetDefaultMessageTemplate() => this.Message;
    }
}
