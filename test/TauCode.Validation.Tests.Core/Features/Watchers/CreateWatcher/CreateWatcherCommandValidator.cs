using FluentValidation;

namespace TauCode.Validation.Tests.Core.Features.Watchers.CreateWatcher
{
    public class CreateWatcherCommandValidator : AbstractValidator<CreateWatcherCommand>
    {
        public CreateWatcherCommandValidator()
        {
            this.CascadeMode = CascadeMode.Stop;

            this.RuleFor(x => x.Guid)
                .NotEmpty()
                .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherGuid);

            this.RuleFor(x => x.BasicCurrencyCode)
                .CurrencyCode();
        }
    }
}
