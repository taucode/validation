using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.SystemWatchers.DeleteSystemWatcher;

namespace TauCode.Validation.Tests.SystemWatchers;

[TestFixture]
public class DeleteCurrencyCommandValidatorTests : ValidatorTestBase<
    DeleteSystemWatcherCommand,
    DeleteSystemWatcherCommandValidator>
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
    public void Id_IsBad_Error(long badId)
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
                nameof(DeleteSystemWatcherCommand.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id.");
    }

    [Test]
    public void Id_IsOfDefaultSystemWatcher_Error()
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
                nameof(DeleteSystemWatcherCommand.Id),
                "NotEqualValidator",
                "'Id' must not be equal to '1'.");
    }

    protected override DeleteSystemWatcherCommand CreateInstance()
    {
        return new DeleteSystemWatcherCommand
        {
            Id = 2L,
        };
    }
}