using FluentValidation;
using FluentValidation.Validators;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core.Validators;

namespace TauCode.Validation.Tests.Core.Features.Quotes.SetWatcherQuote;

public class SetWatcherQuoteCommandValidator : SinglePropertyValidator<SetWatcherQuoteCommand>, IParameterValidator
{
    public SetWatcherQuoteCommandValidator()
        : base(
            new Dictionary<string, IPropertyValidator>
            {
                { nameof(SetWatcherQuoteCommand.Rate), new QuoteRateValidator<SetWatcherQuoteCommand>() },
                { nameof(SetWatcherQuoteCommand.SystemWatcherId), new LongIdValidator<SetWatcherQuoteCommand>() },
            },
            "Command")
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => this.GetWatcherId())
            .LongId()
            .WithName(nameof(SetWatcherQuoteCommand.WatcherId));

        this.RuleFor(x => x.CurrencyCode)
            .CurrencyCode();

        this.RuleFor(x => x.Date)
            .WrapValueTypeValidator(new QuoteDateValidator<SetWatcherQuoteCommand>());
    }

    private long GetWatcherId() =>
        this.Parameters?.GetDictionaryValueOrDefault("id") as long? ?? CoreConstants.NullLongId;

    public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}