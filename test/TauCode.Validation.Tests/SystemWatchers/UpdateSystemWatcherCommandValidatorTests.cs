using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.SystemWatchers.UpdateSystemWatcher;

namespace TauCode.Validation.Tests.SystemWatchers;

[TestFixture]
public class UpdateSystemWatcherCommandValidatorTests : ValidatorTestBase<
    UpdateSystemWatcherCommand,
    UpdateSystemWatcherCommandValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
        this.Validator.Parameters["id"] = 2L;
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
    public void Id_IsBad_Error(long? badId)
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
                nameof(UpdateSystemWatcherCommand.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id.");
    }

    [Test]
    public void Id_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["id"] = DataConstants.SystemWatcher.DefaultSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateSystemWatcherCommand.Id),
                "NotEqualValidator",
                $"'Id' must not be equal to '{DataConstants.SystemWatcher.DefaultSystemWatcherId}'.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void Code_IsNullOrEmpty_Error(string watcherCode)
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
                nameof(UpdateSystemWatcherCommand.Code),
                "SystemWatcherCodeValidator",
                "'Code' must be a valid System Watcher code.");
    }

    [Test]
    [TestCase(" acme")]
    [TestCase("Acme")]
    [TestCase("too-long-system-watcher-code-you-should-not-really-use-long-names-like-this-please-try-using-shorter-names")]
    public void Code_IsInvalid_Error(string watcherCode)
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
                nameof(UpdateSystemWatcherCommand.Code),
                "SystemWatcherCodeValidator",
                "'Code' must be a valid System Watcher code.");
    }

    [Test]
    public void Code_IsOfDefaultSystemWatcher_Error()
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
                nameof(UpdateSystemWatcherCommand.Code),
                "NotEqualValidator",
                $"'Code' must not be equal to '{DataConstants.SystemWatcher.DefaultSystemWatcherCode}'.");
    }


    protected override UpdateSystemWatcherCommand CreateInstance()
    {
        return new UpdateSystemWatcherCommand
        {
            Code = "new-code",
        };
    }
}