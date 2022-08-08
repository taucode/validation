using FluentValidation;
using TauCode.Extensions;

namespace TauCode.Validation.Tests.Core.Features.Watchers.SetupWatcherCurrency;

public class SetupWatcherCurrencyCommandValidator : AbstractValidator<SetupWatcherCurrencyCommand>, IParameterValidator
{
    public SetupWatcherCurrencyCommandValidator()
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => this.GetWatcherId())
            .LongId()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId)
            .WithName(nameof(SetupWatcherCurrencyCommand.WatcherId));

        this.RuleFor(x => x.CurrencyCode)
            .CurrencyCode();
    }

    private long GetWatcherId() =>
        this.Parameters?.GetDictionaryValueOrDefault("watcherId") as long? ?? CoreConstants.NullLongId;

    public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}