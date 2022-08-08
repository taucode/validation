using FluentValidation;
using FluentValidation.Validators;
using System.Reflection;
using System.Text;

namespace TauCode.Validation;

public class ValueTypePropertyValidatorWrapper<T, TProperty> : PropertyValidator<T, TProperty?>
    where TProperty : struct
{
    private readonly PropertyValidator<T, TProperty> _sourceValidator;

    public ValueTypePropertyValidatorWrapper(PropertyValidator<T, TProperty> sourceValidator)
    {
        _sourceValidator = sourceValidator;
    }

    public override bool IsValid(ValidationContext<T> context, TProperty? value)
    {
        if (value.HasValue)
        {
            return _sourceValidator.IsValid(context, value.Value);
        }

        return true;
    }

    public override string Name => _sourceValidator.Name;

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        MethodInfo sourceMethod =
            _sourceValidator.GetType().GetMethod(
                nameof(GetDefaultMessageTemplate),
                BindingFlags.Instance | BindingFlags.NonPublic,
                Type.DefaultBinder,
                new[] { typeof(string) },
                null);

        if (sourceMethod == null)
        {
            throw new NotImplementedException();
        }

        var baseMsg = (string)sourceMethod.Invoke(_sourceValidator, new object[] { errorCode });

        var sb = new StringBuilder(baseMsg);
        sb.Append(" This property is optional and can be null.");

        return sb.ToString();
    }
}