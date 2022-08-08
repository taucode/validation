using NUnit.Framework;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features;
using TauCode.Validation.Tests.Core.Features.Quotes.SetSystemWatcherQuotes;

namespace TauCode.Validation.Tests.Quotes;

[TestFixture]
[SetCulture("")]
public class SetSystemWatcherQuotesCommandValidatorTests : ValidatorTestBase<
    SetSystemWatcherQuotesCommand,
    SetSystemWatcherQuotesCommandValidator>
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
    public void Command_IsValid_RunsOk(string dateString)
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
    public void WatcherId_IsBad_Error(long? badId)
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["id"] = badId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetSystemWatcherQuotesCommand.WatcherId),
                "LongIdValidator",
                "'WatcherId' must be a valid long Id.");
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
                nameof(SetSystemWatcherQuotesCommand.Date),
                "QuoteDateValidator",
                "'Date' must be an exact date within range 1900-01-01..2999-12-31. This property is optional and can be null.");
    }

    [Test]
    public void CurrencyRates_IsNullOrEmpty_Error()
    {
        // Arrange
        var command1 = this.CreateInstance();
        command1.CurrencyRates = null;

        var command2 = this.CreateInstance();
        command2.CurrencyRates = new List<SetupSystemCurrencyRate>();

        // Act
        var validationResult1 = this.ValidateInstance(command1);
        var validationResult2 = this.ValidateInstance(command2);

        // Assert
        var validationResults = new[]
        {
            validationResult1,
            validationResult2,
        };

        foreach (var validationResult in validationResults)
        {
            validationResult.ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    nameof(SetSystemWatcherQuotesCommand.CurrencyRates),
                    "NotEmptyValidator",
                    "'Currency Rates' must not be empty.");
        }
    }

    [Test]
    public void CurrencyRates_ContainsNull_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.CurrencyRates[idx] = null;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}[{idx}]",
                "NotNullValidator",
                $"'{nameof(SetupSystemCurrencyRate)}' must not be empty.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("RB", Description = "Not 3 symbols")]
    [TestCase("EURO", Description = "Not 3 symbols")]
    [TestCase("USd", Description = "Not all upper-case")]
    [TestCase("RB.", Description = "Not all letters")]
    public void CurrencyRates_CurrencyCodeIsInvalid_Error(string code)
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.CurrencyRates[idx].CurrencyCode = code;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.CurrencyCode)}",
                "CurrencyCodeValidator",
                "'Currency Code' must be a valid currency code.");
    }

    [Test]
    [TestCase("0")]
    [TestCase("-0.1")]
    public void CurrencyRates_RateIsInvalid_Error(string rateString)
    {
        // Arrange
        var rate = rateString.ToDecimal();
        var command = this.CreateInstance();
        const int idx = 1;
        command.CurrencyRates[idx].Rate = rate;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.Rate)}",
                "QuoteRateValidator",
                $"'{nameof(SetupSystemCurrencyRate.Rate)}' must be positive.");
    }

    [Test]
    public void CurrencyRates_NonUniqueCurrencyCodes_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 0;
        command.CurrencyRates[idx].CurrencyCode = "UAH";

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetSystemWatcherQuotesCommand.CurrencyRates),
                "PredicateValidator",
                $"'{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}' must contain unique currency codes.");
    }

    [Test]
    public void CurrencyRates_ContainsUsd_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.CurrencyRates[idx].CurrencyCode = "USD";

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(SetSystemWatcherQuotesCommand.CurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.CurrencyCode)}",
                "NotEqualValidator",
                "'Currency Code' must not be equal to 'USD'.");
    }

    protected override SetSystemWatcherQuotesCommand CreateInstance()
    {
        return new SetSystemWatcherQuotesCommand
        {
            Date = "2019-03-03Z".ToUtcDateOffset(),
            CurrencyRates = new List<SetupSystemCurrencyRate>()
            {
                new SetupSystemCurrencyRate
                {
                    CurrencyCode = "EUR",
                    Rate = 0.91m,
                },
                new SetupSystemCurrencyRate
                {
                    CurrencyCode = "UAH",
                    Rate = 31.3m,
                },
            },
        };
    }
}