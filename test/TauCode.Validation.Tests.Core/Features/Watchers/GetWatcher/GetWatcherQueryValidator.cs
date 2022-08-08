using FluentValidation;
using FluentValidation.Validators;

namespace TauCode.Validation.Tests.Core.Features.Watchers.GetWatcher;

public class GetWatcherQueryValidator : SinglePropertyValidator<GetWatcherQuery>
{
    public GetWatcherQueryValidator()
        : base(new Dictionary<string, IPropertyValidator>
        {
            { nameof(GetWatcherQuery.Id), new LongIdValidator<GetWatcherQuery>() },
            { nameof(GetWatcherQuery.Guid), new NullableNotEmptyValidator<GetWatcherQuery, Guid>() },
        })
    {
        this.CascadeMode = CascadeMode.Stop;

        this.RuleFor(x => x.Id)
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherId);

        this.RuleFor(x => x.Guid)
            .NotEqual(DataConstants.SystemWatcher.DefaultSystemWatcherGuid);
    }
}