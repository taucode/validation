using FluentValidation;

namespace TauCode.Validation.Tests.Core.Features.Watchers.DeleteWatcher
{
    public class DeleteWatcherCommandValidator : AbstractValidator<DeleteWatcherCommand>
    {
        public DeleteWatcherCommandValidator()
        {
            this.CascadeMode = CascadeMode.Stop;

            this.RuleFor(x => x.Id)
                .LongId()
                .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId)
                .WithName(nameof(DeleteWatcherCommand.Id));
        }
    }
}
