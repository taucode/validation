using FluentValidation;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core.Validators;

namespace TauCode.Validation.Tests.Core.Features.Quotes.SetSystemWatcherQuotes;

public class SetSystemWatcherQuotesCommandValidator :
    AbstractValidator<SetSystemWatcherQuotesCommand>,
    IParameterValidator
{
    #region Constructor

    public SetSystemWatcherQuotesCommandValidator()
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => this.GetWatcherId())
            .LongId()
            .WithName(nameof(SetSystemWatcherQuotesCommand.WatcherId));

        this.RuleFor(x => x.Date)
            .WrapValueTypeValidator(new QuoteDateValidator<SetSystemWatcherQuotesCommand>());
        //.NullableQuoteDate();

        this.RuleFor(x => x.CurrencyRates)
            .NotEmpty()
            .Must(CoreHelper.UniqueCurrencyCodes)
            .WithMessage(
                $"'{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}' must contain unique currency codes.");

        this.RuleForEach(x => x.CurrencyRates)
            .NotNull()
            .WithName(nameof(SetupSystemCurrencyRate))
            .SetValidator(new SetupSystemCurrencyRateValidator());
    }

    #endregion

    #region Private

    private long GetWatcherId() =>
        this.Parameters?.GetDictionaryValueOrDefault("id") as long? ?? CoreConstants.NullLongId;

    #endregion

    #region IParameterValidator Members

    public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

    #endregion
}