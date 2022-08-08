using FluentValidation;
using TauCode.Validation.Tests.Core.Validators;

namespace TauCode.Validation.Tests.Core.Features.SystemWatchers.CreateSystemWatcher;

public class CreateSystemWatcherCommandValidator : AbstractValidator<CreateSystemWatcherCommand>
{
    public CreateSystemWatcherCommandValidator()
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => x.Code)
            .SystemWatcherCode()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherCode);

        this.RuleFor(x => x.Guid)
            .NotEmpty()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherGuid);

        this.RuleFor(x => x.InitialCurrencyRates)
            .NotEmpty()
            .Must(CoreHelper.UniqueCurrencyCodes)
            .WithMessage($"'{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}' must contain unique currency codes.");

        this.RuleForEach(x => x.InitialCurrencyRates)
            .NotNull()
            .WithName(nameof(SetupSystemCurrencyRate))
            .SetValidator(new SetupSystemCurrencyRateValidator());
    }
}