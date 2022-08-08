using FluentValidation;
using FluentValidation.Results;

namespace TauCode.Validation.Tests.Core;

public abstract class ValidatorTestBase<T, TValidator> where TValidator : IValidator<T>
{
    protected TValidator Validator { get; set; }

    public virtual void SetUpImpl()
    {
        this.Validator = this.CreateValidator();
    }

    protected ValidationResult ValidateInstance(T instance)
    {
        var validationResult = this.Validator.Validate(instance);
        return validationResult;
    }

    protected virtual TValidator CreateValidator()
    {
        var ctor = typeof(TValidator).GetConstructor(Type.EmptyTypes);
        if (ctor == null)
        {
            throw new InvalidOperationException($"No parameterless constructor defined for type '{typeof(TValidator).FullName}'.");
        }

        var validator = (TValidator)ctor.Invoke(new object[0]);
        return validator;
    }

    protected abstract T CreateInstance();
}