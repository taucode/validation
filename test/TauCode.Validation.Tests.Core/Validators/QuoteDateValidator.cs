namespace TauCode.Validation.Tests.Core.Validators;

public class QuoteDateValidator<T> : ExactUtcDateValidator<T>
{
    public QuoteDateValidator()
        : base(DataConstants.DateAndTime.MinDate, DataConstants.DateAndTime.MaxDate)
    {
    }

    public override string Name => "QuoteDateValidator";
}