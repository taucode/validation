using FluentValidation;

namespace TauCode.Validation.Tests.Core.Features.Watchers.RemoveWatcherCurrency;

public class RemoveWatcherCurrencyCommandValidator : AbstractValidator<RemoveWatcherCurrencyCommand>
{
    public RemoveWatcherCurrencyCommandValidator()
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => x.WatcherId)
            .LongId()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId);

        this.RuleFor(x => x.CurrencyCode)
            .CurrencyCode();
    }
}