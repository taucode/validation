using FluentValidation;
using FluentValidation.Validators;
using System.Reflection;
using System.Text;

namespace TauCode.Validation;

public abstract class SinglePropertyValidator<T> : AbstractValidator<T>
{
    #region Nested

    private class PropertyAsObjectValidator : PropertyValidator<T, object>
    {
        private readonly IPropertyValidator _inner;

        internal PropertyAsObjectValidator(IPropertyValidator inner)
        {
            _inner = inner;
        }

        public override bool IsValid(ValidationContext<T> context, object? value)
        {
            if (value == null)
            {
                return true; // null is acceptable, that's the whole point.
            }

            var method = _inner.GetType().GetMethod(nameof(IsValid));

            if (method == null)
            {
                throw new MissingMethodException(this.GetType().FullName, nameof(IsValid));
            }

            var res = (bool)(
                method.Invoke(_inner, new object[] { context, value })
                ??
                // should never happen, actually.
                throw new NullReferenceException($"Method '{nameof(IsValid)}' returned null instead of boolean value."));

            return res;
        }

        public override string Name => _inner.Name;

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            var baseMsg = _inner.GetDefaultMessageTemplate(errorCode);

            var sb = new StringBuilder(baseMsg);
            sb.Append(" This property is optional and can be null.");

            return sb.ToString();
        }
    }

    #endregion

    private readonly IDictionary<string, PropertyInfo> _propertyInfos;

    protected SinglePropertyValidator(IDictionary<string, IPropertyValidator> propertyValidators, string objectName = "Query")
    {
        // todo: check args, incl. not empty collection

        this.RuleLevelCascadeMode = CascadeMode.Stop;

        _propertyInfos = this.BuildPropertyInfos(propertyValidators.Keys);

        var message = this.BuildMessage();

        this.RuleFor(x => x)
            .Must(ExactlyOnePropertyProvided)
            .WithErrorCode(this.GetType().Name)
            .WithName(objectName)
            .WithMessage(message);

        foreach (var pair in propertyValidators)
        {
            var propertyName = pair.Key;
            var propertyValidator = pair.Value;

            this.RuleFor(x => this.GetProperty(x, propertyName))
                .SetValidator(new PropertyAsObjectValidator(propertyValidator))
                .WithErrorCode(propertyValidator.Name)
                .WithName(propertyName);
        }
    }

    private object GetProperty(object instance, string propertyName)
    {
        var propertyInfo = _propertyInfos[propertyName]; // todo checks
        var propertyValue = propertyInfo.GetValue(instance);

        return propertyValue;
    }

    private IDictionary<string, PropertyInfo> BuildPropertyInfos(IEnumerable<string> propertyNames)
    {
        var propertyInfos = new Dictionary<string, PropertyInfo>();

        foreach (var propertyName in propertyNames)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new NotImplementedException(); // todo
            }

            propertyInfos.Add(propertyName, propertyInfo);
        }

        return propertyInfos;
    }

    private bool ExactlyOnePropertyProvided(T obj)
    {
        var gotNonNull = false;

        foreach (var pair in _propertyInfos)
        {
            var propertyInfo = pair.Value;

            var propertyValue = propertyInfo.GetValue(obj);
            if (propertyValue == null)
            {
                // keep trying...
            }
            else
            {
                if (gotNonNull)
                {
                    return false;
                }
                else
                {
                    gotNonNull = true;
                }
            }
        }

        return gotNonNull;
    }

    private string BuildMessage()
    {
        var sb = new StringBuilder();

        sb.Append("Exactly one property of the following should be provided: ");

        var propertyNames = _propertyInfos.Keys.ToList();
        for (var i = 0; i < propertyNames.Count; i++)
        {
            var propertyName = propertyNames[i];
            sb.Append('\'');
            sb.Append(propertyName);
            sb.Append('\'');

            if (i < propertyNames.Count - 1)
            {
                sb.Append(", ");
            }
        }

        sb.Append('.');

        return sb.ToString();
    }
}