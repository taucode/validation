using FluentValidation;
using System.Collections.Generic;
using TauCode.Extensions;

namespace TauCode.Validation.Tests.Core.Features.Currencies.UpdateCurrency
{
    public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>, IParameterValidator
    {
        public UpdateCurrencyCommandValidator()
        {
            this.CascadeMode = CascadeMode.Stop;

            this.RuleFor(x => this.GetId())
                .LongId()
                .NotPredefinedCurrencyId()
                .WithName(nameof(UpdateCurrencyCommand.Id));

            this.RuleFor(x => x.Code)
                .CurrencyCode()
                .NotPredefinedCurrencyCode();

            this.RuleFor(x => x.Name)
                .FullName(1, DataConstants.Currency.MaxCurrencyNameLength, false);
        }

        private long GetId() =>
            this.Parameters?.GetDictionaryValueOrDefault("id") as long? ?? CoreConstants.NullLongId;

        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
