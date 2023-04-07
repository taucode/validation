using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Currencies.DeleteCurrency;

namespace TauCode.Validation.Tests.Currencies;

[TestFixture]
public class DeleteCurrencyCommandValidatorTests : ValidatorTestBase<
    DeleteCurrencyCommand,
    DeleteCurrencyCommandValidator>
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
                nameof(DeleteCurrencyCommand.Id),
                "LongIdValidator",
                "'Id' must be a valid Id.");
    }

    [Test]
    [TestCaseSource(typeof(TestHelper), nameof(TestHelper.GetPredefinedCurrencyIds))]
    public void Id_IsPredefined_Error(long id)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Id = id;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(DeleteCurrencyCommand.Id),
                "NotPredefinedCurrencyIdValidator",
                "'Id' must not designate a pre-defined currency Id.");
    }

    protected override DeleteCurrencyCommand CreateInstance()
    {
        return new DeleteCurrencyCommand
        {
            Id = 9999L,  // random currency id
        };
    }
}