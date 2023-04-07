using NUnit.Framework;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Quotes.SetWatcherQuote;

namespace TauCode.Validation.Tests.Quotes;

[TestFixture]
public class SetWatcherQuoteCommandValidatorTests : ValidatorTestBase<
    SetWatcherQuoteCommand,
    SetWatcherQuoteCommandValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
        this.Validator.Parameters["id"] = 101L;
    }

    [Test]
    [TestCase(null)]
    [TestCase("2012-01-02Z")]
    public void Command_IsValid_Ok(string dateString)
    {
        // Arrange
        var date = dateString.ToNullableUtcDateOffset();
        var command = this.CreateInstance();
        command.Date = date;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeValid();
    }

    [Test]
    [TestCase(null)]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void WatcherId_IsBad_Error(long? badWatcherId)
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["id"] = badWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetWatcherQuoteCommand.WatcherId),
                "LongIdValidator",
                "'WatcherId' must be a valid Id.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("RB", Description = "Not 3 symbols")]
    [TestCase("EURO", Description = "Not 3 symbols")]
    [TestCase("USd", Description = "Not all upper-case")]
    [TestCase("RB.", Description = "Not all letters")]
    public void CurrencyCode_IsBad_Error(string badCurrencyCode)
    {
        // Arrange
        var command = this.CreateInstance();
        command.CurrencyCode = badCurrencyCode;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetWatcherQuoteCommand.CurrencyCode),
                "CurrencyCodeValidator",
                "'Currency Code' must be a valid currency code.");
    }

    [Test]
    [TestCase("1899-01-01Z", Description = "Before beginning of the time")]
    [TestCase("3000-01-01Z", Description = "After end of the time")]
    [TestCase("2019-02-01 02:00:00 +02:00", Description = "UTC date is exact but shift is not a zero timespan")]
    [TestCase("2019-02-01 02:00:00 +00:00", Description = "Time of day is not zero")]
    public void Date_IsInvalid_Error(string dateString)
    {
        // Arrange
        var date = DateTimeOffset.Parse(dateString);
        var command = this.CreateInstance();
        command.Date = date;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetWatcherQuoteCommand.Date),
                "QuoteDateValidator",
                "'Date' must be an exact date within range 1900-01-01..2999-12-31. This property is optional and can be null.");
    }

    [Test]
    [TestCase("0")]
    [TestCase("-0.1")]
    public void Rate_IsInvalid_Error(string rateString)
    {
        // Arrange
        var rate = rateString.ToDecimal();
        var command = this.CreateInstance();
        command.Rate = rate;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetWatcherQuoteCommand.Rate),
                "QuoteRateValidator",
                "'Rate' must be positive. This property is optional and can be null.");
    }

    [Test]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void SystemWatcherId_IsBad_Error(long badSystemWatcherId)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Rate = null;
        command.SystemWatcherId = badSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetWatcherQuoteCommand.SystemWatcherId),
                "LongIdValidator",
                "'SystemWatcherId' must be a valid Id. This property is optional and can be null.");
    }

    [Test]
    [TestCase("14.88", null)]
    [TestCase(null, 2L)]
    public void RateAndSystemWatcherId_OnlyOneProvided_RunsOk(string rateString, long? systemWatcherId)
    {
        // Arrange
        var rate = rateString?.ToDecimal();

        var command = this.CreateInstance();

        command.Rate = rate;
        command.SystemWatcherId = systemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeValid();
    }

    [Test]
    [TestCase("14.88", 1L)]
    [TestCase(null, null)]
    public void RateAndSystemWatcherId_BothProvided_Error(string rateString, long? systemWatcherId)
    {
        // Arrange
        var rate = rateString?.ToDecimal();

        var command = this.CreateInstance();

        command.Rate = rate;
        command.SystemWatcherId = systemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                "Command",
                nameof(SetWatcherQuoteCommandValidator),
                "Exactly one property of the following should be provided: 'Rate', 'SystemWatcherId'.");
    }

    protected override SetWatcherQuoteCommand CreateInstance()
    {

        return new SetWatcherQuoteCommand
        {
            CurrencyCode = DataConstants.Currency.UsdCode,
            Date = "2019-03-03Z".ToUtcDateOffset(),
            Rate = 14.88m,
            SystemWatcherId = null,
        };
    }
}