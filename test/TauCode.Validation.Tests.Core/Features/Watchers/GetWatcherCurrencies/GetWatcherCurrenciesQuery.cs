using System;

namespace TauCode.Validation.Tests.Core.Features.Watchers.GetWatcherCurrencies
{
    public class GetWatcherCurrenciesQuery
    {
        public long WatcherId { get; set; }
        public DateTimeOffset? Date { get; set; }
    }
}
