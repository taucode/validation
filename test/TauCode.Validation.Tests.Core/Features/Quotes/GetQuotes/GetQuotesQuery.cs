using System;

namespace TauCode.Validation.Tests.Core.Features.Quotes.GetQuotes
{
    public class GetQuotesQuery
    {
        public long WatcherId { get; set; }

        public DateTimeOffset? Date { get; set; }
    }
}
