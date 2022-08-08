using FluentValidation;
using TauCode.Extensions;

namespace TauCode.Validation.Tests.Core.Features.SystemWatchers.UpdateSystemWatcher;

public class UpdateSystemWatcherCommandValidator :
    AbstractValidator<UpdateSystemWatcherCommand>,
    IParameterValidator
{
    public UpdateSystemWatcherCommandValidator()
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => this.GetId())
            .LongId()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId)
            .WithName(nameof(UpdateSystemWatcherCommand.Id));

        this.RuleFor(x => x.Code)
            .SystemWatcherCode()
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherCode);
    }

    private long GetId() => this.Parameters?.GetDictionaryValueOrDefault("id") as long? ?? CoreConstants.NullLongId;

    public IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}