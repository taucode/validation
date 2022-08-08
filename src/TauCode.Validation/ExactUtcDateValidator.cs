using FluentValidation;
using FluentValidation.Validators;
using TauCode.Extensions;

namespace TauCode.Validation;

public class ExactUtcDateValidator<T> : PropertyValidator<T, DateTimeOffset>
{
    private const string Format = "yyyy-MM-dd";

    public DateTimeOffset? MinDate { get; }
    public DateTimeOffset? MaxDate { get; }
    public const string LimitsDescriptionArgumentName = "limitsDescription";


    public ExactUtcDateValidator(DateTimeOffset? minDate, DateTimeOffset? maxDate)
    {
        if (minDate.HasValue && !minDate.Value.IsUtcDateOffset())
        {
            throw new ArgumentException($"When '{nameof(minDate)}' is provided, it must represent an exact UTC date.", nameof(minDate));
        }

        if (maxDate.HasValue && !maxDate.Value.IsUtcDateOffset())
        {
            throw new ArgumentException($"When '{nameof(maxDate)}' is provided, it must represent an exact UTC date.", nameof(maxDate));
        }

        if (minDate.HasValue && maxDate.HasValue && minDate.Value > maxDate.Value)
        {
            throw new ArgumentException(
                $"When both '{nameof(minDate)}' and '{nameof(maxDate)}' are provided, they must be sequential.",
                nameof(maxDate));
        }

        this.MinDate = minDate;
        this.MaxDate = maxDate;

    }

    public override bool IsValid(ValidationContext<T> context, DateTimeOffset value)
    {
        var valid = true;
        var date = value;

        do
        {
            // must be exact
            if (date.TimeOfDay != TimeSpan.Zero)
            {
                valid = false;
                break;
            }

            // if min date provided, must adhere
            if (this.MinDate.HasValue)
            {
                if (date < this.MinDate.Value)
                {
                    valid = false;
                    break;
                }
            }

            // if max date provided, must adhere
            if (this.MaxDate.HasValue)
            {
                if (date > this.MaxDate.Value)
                {
                    valid = false;
                    break;
                }
            }

        }
        while (false);

        if (!valid)
        {
            context.MessageFormatter.AppendArgument(
                LimitsDescriptionArgumentName,
                this.BuildLimitsDescription());
        }



        return valid;
    }

    public string BuildLimitsDescription()
    {
        string limitsDescription;

        if (this.MinDate.HasValue && !this.MaxDate.HasValue)
        {
            // this.MinDate..∞
            limitsDescription = $" not less than {this.MinDate.Value.ToString(Format)}";
        }
        else if (!this.MinDate.HasValue && this.MaxDate.HasValue)
        {
            // -∞..this.MaxDate
            limitsDescription = $" not greater than {this.MaxDate.Value.ToString(Format)}";
        }
        else if (this.MinDate.HasValue && this.MaxDate.HasValue)
        {
            // this.MinDate..this.MaxDate

            if (this.MinDate.Value == this.MaxDate.Value)
            {
                limitsDescription = $" equal to {this.MinDate.Value.ToString(Format)}";
            }
            else
            {
                limitsDescription = $" within range {this.MinDate.Value.ToString(Format)}..{this.MaxDate.Value.ToString(Format)}";
            }
        }
        else
        {
            // -∞..∞
            limitsDescription = "";
        }

        return limitsDescription;
    }

    public override string Name => "ExactUtcDateValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"'{{PropertyName}}' must be an exact date{{{LimitsDescriptionArgumentName}}}.";
    }
}