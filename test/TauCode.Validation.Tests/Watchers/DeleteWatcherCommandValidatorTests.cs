using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.DeleteWatcher;

namespace TauCode.Validation.Tests.Watchers;

[TestFixture]
public class DeleteWatcherCommandValidatorTests : ValidatorTestBase<
    DeleteWatcherCommand,
    DeleteWatcherCommandValidator>
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
        command.Id = badId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(DeleteWatcherCommand.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id.");
    }

    [Test]
    public void WatcherId_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var command = this.CreateInstance();
        command.Id = DataConstants.SystemWatcher.DefaultSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(DeleteWatcherCommand.Id),
                "NotEqualValidator",
                "'Id' must not be equal to '1'.");
    }

    protected override DeleteWatcherCommand CreateInstance()
    {
        return new DeleteWatcherCommand
        {
            Id = 100801L,
        };
    }
}