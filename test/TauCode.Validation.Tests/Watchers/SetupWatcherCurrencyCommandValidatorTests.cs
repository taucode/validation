using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.SetupWatcherCurrency;

namespace TauCode.Validation.Tests.Watchers;

[TestFixture]
public class SetupWatcherCurrencyCommandValidatorTests : ValidatorTestBase<
    SetupWatcherCurrencyCommand,
    SetupWatcherCurrencyCommandValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
        this.Validator.Parameters["watcherId"] = 2L;
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
    [TestCase(null)]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void WatcherId_IsBad_Error(long? badId)
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["watcherId"] = badId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetupWatcherCurrencyCommand.WatcherId),
                "LongIdValidator",
                "'WatcherId' must be a valid Id.");
    }

    [Test]
    public void WatcherId_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["watcherId"] = DataConstants.SystemWatcher.DefaultSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(SetupWatcherCurrencyCommand.WatcherId),
                "NotEqualValidator",
                "'WatcherId' must not be equal to '1'.");
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
                nameof(SetupWatcherCurrencyCommand.CurrencyCode),
                "CurrencyCodeValidator",
                "'Currency Code' must be a valid currency code.");
    }

    protected override SetupWatcherCurrencyCommand CreateInstance()
    {
        return new SetupWatcherCurrencyCommand
        {
            CurrencyCode = "EUR",
        };
    }
}