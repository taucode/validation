namespace TauCode.Validation.Tests.Core.Features.SystemWatchers.CreateSystemWatcher;

public class CreateSystemWatcherCommand
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public IList<SetupSystemCurrencyRate> InitialCurrencyRates { get; set; }
}