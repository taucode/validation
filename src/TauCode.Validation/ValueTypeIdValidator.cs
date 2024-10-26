using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation;

public abstract class ValueTypeIdValidator<TClass, TId> : PropertyValidator<TClass, TId>
    where TId : struct, IComparable<TId>
{
    public override bool IsValid(ValidationContext<TClass> context, TId value)
    {
        return value.CompareTo(default) > 0;
    }

    public override string Name => this.GetType().Name.Split('`').First();

    protected override string GetDefaultMessageTemplate(string errorCode) => "'{PropertyName}' must be a valid Id.";
}