using FluentValidation;
using System.Collections.Generic;
using TauCode.Extensions;

namespace TauCode.Validation.Tests.Core.Features.Watchers.ChangeWatcherBasicCurrency
{
    public class ChangeWatcherBasicCurrencyCommandValidator :
        AbstractValidator<ChangeWatcherBasicCurrencyCommand>,
        IParameterValidator
    {
        public ChangeWatcherBasicCurrencyCommandValidator()
        {
            this.CascadeMode = CascadeMode.Stop;

            this.RuleFor(x => this.GetWatcherId())
                .LongId()
                .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId)
                .WithName(nameof(ChangeWatcherBasicCurrencyCommand.WatcherId));

            this.RuleFor(x => x.CurrencyCode)
                .CurrencyCode();
        }

        private long GetWatcherId() =>
            this.Parameters?.GetDictionaryValueOrDefault("watcherId") as long? ?? CoreConstants.NullLongId;

        public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
