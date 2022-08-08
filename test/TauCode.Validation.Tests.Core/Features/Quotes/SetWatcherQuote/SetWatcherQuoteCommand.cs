namespace TauCode.Validation.Tests.Core.Features.Quotes.SetWatcherQuote;

public class SetWatcherQuoteCommand
{
    public long WatcherId { get; set; }
    public string CurrencyCode { get; set; }
    public DateTimeOffset? Date { get; set; }
    public decimal? Rate { get; set; }
    public long? SystemWatcherId { get; set; }
}