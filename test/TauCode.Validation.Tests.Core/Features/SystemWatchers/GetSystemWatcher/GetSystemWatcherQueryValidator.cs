using FluentValidation;
using FluentValidation.Validators;
using TauCode.Validation.Tests.Core.Validators;

namespace TauCode.Validation.Tests.Core.Features.SystemWatchers.GetSystemWatcher;

public class GetSystemWatcherQueryValidator : SinglePropertyValidator<GetSystemWatcherQuery>
{
    public GetSystemWatcherQueryValidator()
        : base(new Dictionary<string, IPropertyValidator>
        {
            { nameof(GetSystemWatcherQuery.Id), new LongIdValidator<GetSystemWatcherQuery>() },
            { nameof(GetSystemWatcherQuery.Guid), new NullableNotEmptyValidator<GetSystemWatcherQuery, Guid>(/*default(Guid)*/) },
            { nameof(GetSystemWatcherQuery.Code), new SystemWatcherCodeValidator<GetSystemWatcherQuery>() },
        })
    {
        this.CascadeMode = CascadeMode.Stop;
    }
}