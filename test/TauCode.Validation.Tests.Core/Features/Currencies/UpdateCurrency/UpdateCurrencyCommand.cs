namespace TauCode.Validation.Tests.Core.Features.Currencies.UpdateCurrency
{
    public class UpdateCurrencyCommand
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool AvailableToWatchers { get; set; }
    }
}
