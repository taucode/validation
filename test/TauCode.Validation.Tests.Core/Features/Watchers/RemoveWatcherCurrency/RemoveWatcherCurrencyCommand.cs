namespace TauCode.Validation.Tests.Core.Features.Watchers.RemoveWatcherCurrency
{
    public class RemoveWatcherCurrencyCommand
    {
        public long WatcherId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
