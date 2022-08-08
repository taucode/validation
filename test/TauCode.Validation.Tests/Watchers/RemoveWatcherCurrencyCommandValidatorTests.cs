using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.RemoveWatcherCurrency;

namespace TauCode.Validation.Tests.Watchers;

[TestFixture]
public class RemoveWatcherCurrencyCommandValidatorTests : ValidatorTestBase<
    RemoveWatcherCurrencyCommand,
    RemoveWatcherCurrencyCommandValidator>
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
    [TestCase(0L)]
    [TestCase(-1L)]
    public void WatcherId_IsBad_Error(long badId)
    {
        // Arrange
        var command = this.CreateInstance();
        command.WatcherId = badId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(RemoveWatcherCurrencyCommand.WatcherId),
                "LongIdValidator",
                "'Watcher Id' must be a valid long Id.");
    }

    [Test]
    public void WatcherId_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        command.WatcherId = DataConstants.SystemWatcher.DefaultSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(RemoveWatcherCurrencyCommand.WatcherId),
                "NotEqualValidator",
                "'Watcher Id' must not be equal to '1'.");
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
                nameof(RemoveWatcherCurrencyCommand.CurrencyCode),
                "CurrencyCodeValidator",
                "'Currency Code' must be a valid currency code.");
    }

    protected override RemoveWatcherCurrencyCommand CreateInstance()
    {
        return new RemoveWatcherCurrencyCommand
        {
            WatcherId = 12L,
            CurrencyCode = "ZAZ", // fictional currency code
        };
    }
}