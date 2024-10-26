using NUnit.Framework;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features;
using TauCode.Validation.Tests.Core.Features.SystemWatchers.CreateSystemWatcher;

namespace TauCode.Validation.Tests.SystemWatchers;

[TestFixture]
[SetCulture("")]
public class CreateSystemWatcherCommandValidatorTests : ValidatorTestBase<
    CreateSystemWatcherCommand,
    CreateSystemWatcherCommandValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
    }

    [Test]
    public void Command_IsValid_Ok()
    {
        // Arrange
        var command = this.CreateInstance();

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeValid();
    }

    [Test]
    public void WatcherGuid_IsEmpty_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        command.Guid = Guid.Empty;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.Guid),
                "NotEmptyValidator",
                "'Guid' must not be empty.");
    }

    [Test]
    public void WatcherGuid_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        command.Guid = DataConstants.SystemWatcher.DefaultSystemWatcherGuid;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.Guid),
                "NotEqualValidator",
                $"'Guid' must not be equal to '{DataConstants.SystemWatcher.DefaultSystemWatcherGuid}'.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void WatcherCode_IsNullOrEmpty_Error(string? watcherCode)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Code = watcherCode;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.Code),
                "SystemWatcherCodeValidator",
                "'Code' must be a valid System Watcher code.");
    }

    [Test]
    public void WatcherCode_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        command.Code = DataConstants.SystemWatcher.DefaultSystemWatcherCode;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.Code),
                "NotEqualValidator",
                "'Code' must not be equal to 'default'.");
    }

    [Test]
    [TestCase(" acme")]
    [TestCase("Acme")]
    [TestCase("acme_wrong_delimiter")]
    [TestCase(
        "too-long-system-watcher-code-you-should-not-really-use-long-names-like-this-please-try-using-shorter-names")]
    public void WatcherCode_IsInvalid_Error(string watcherCode)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Code = watcherCode;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.Code),
                "SystemWatcherCodeValidator",
                "'Code' must be a valid System Watcher code.");
    }

    [Test]
    public void InitialCurrencyRates_IsNullOrEmpty_Error()
    {
        // Arrange
        var command1 = this.CreateInstance();
        command1.InitialCurrencyRates = null;

        var command2 = this.CreateInstance();
        command2.InitialCurrencyRates = new List<SetupSystemCurrencyRate>();

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
                    nameof(CreateSystemWatcherCommand.InitialCurrencyRates),
                    "NotEmptyValidator",
                    "'Initial Currency Rates' must not be empty.");
        }
    }

    [Test]
    public void InitialCurrencyRates_ContainsNull_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.InitialCurrencyRates[idx] = null;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}[{idx}]",
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
    public void InitialCurrencyRates_CurrencyCodeIsInvalid_Error(string? code)
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.InitialCurrencyRates[idx].CurrencyCode = code;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.CurrencyCode)}",
                "CurrencyCodeValidator",
                "'Currency Code' must be a valid currency code.");
    }

    [Test]
    [TestCase("0")]
    [TestCase("-0.1")]
    public void InitialCurrencyRates_RateIsInvalid_Error(string rateString)
    {
        // Arrange
        var rate = rateString.ToDecimal();
        var command = this.CreateInstance();
        const int idx = 1;
        command.InitialCurrencyRates[idx].Rate = rate;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.Rate)}",
                "QuoteRateValidator",
                $"'{nameof(SetupSystemCurrencyRate.Rate)}' must be positive.");
    }

    [Test]
    public void InitialCurrencyRates_NonUniqueCurrencyCodes_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 0;
        command.InitialCurrencyRates[idx].CurrencyCode = "UAH";

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(CreateSystemWatcherCommand.InitialCurrencyRates),
                "PredicateValidator",
                $"'{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}' must contain unique currency codes.");
    }

    [Test]
    public void InitialCurrencyRates_ContainsUsd_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        const int idx = 1;
        command.InitialCurrencyRates[idx].CurrencyCode = "USD";

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                $"{nameof(CreateSystemWatcherCommand.InitialCurrencyRates)}[{idx}].{nameof(SetupSystemCurrencyRate.CurrencyCode)}",
                "NotEqualValidator",
                "'Currency Code' must not be equal to 'USD'.");
    }

    protected override CreateSystemWatcherCommand CreateInstance()
    {
        return new CreateSystemWatcherCommand
        {
            Code = "acme-my-1",
            Guid = new Guid("6e3042a6-7526-4960-a3d2-bf1335125473"),
            InitialCurrencyRates = new List<SetupSystemCurrencyRate>
            {
                new SetupSystemCurrencyRate
                {
                    CurrencyCode = "EUR",
                    Rate = 0.88m,
                },
                new SetupSystemCurrencyRate
                {
                    CurrencyCode = "UAH",
                    Rate = 14.88m,
                },
            },
        };
    }
}