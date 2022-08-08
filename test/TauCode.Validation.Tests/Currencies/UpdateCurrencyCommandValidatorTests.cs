using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Currencies.UpdateCurrency;

namespace TauCode.Validation.Tests.Currencies;

[TestFixture]
public class UpdateCurrencyCommandValidatorTests : ValidatorTestBase<
    UpdateCurrencyCommand,
    UpdateCurrencyCommandValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
        this.Validator.Parameters["id"] = 9999L; // real-like currency id
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
    public void Id_IsInvalid_Error(long? badId)
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["id"] = null;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateCurrencyCommand.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id.");
    }

    [Test]
    [TestCaseSource(typeof(TestHelper), nameof(TestHelper.GetPredefinedCurrencyIds))]
    public void Id_IsPredefined_Error(long id)
    {
        // Arrange
        var command = this.CreateInstance();
        this.Validator.Parameters["id"] = id;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateCurrencyCommand.Id),
                "NotPredefinedCurrencyIdValidator",
                "'Id' must not designate a pre-defined currency Id.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("RB", Description = "Not 3 symbols")]
    [TestCase("EURO", Description = "Not 3 symbols")]
    [TestCase("USd", Description = "Not all upper-case")]
    [TestCase("RB.", Description = "Not all letters")]
    public void Code_IsInvalid_Error(string code)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Code = code;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateCurrencyCommand.Code),
                "CurrencyCodeValidator",
                "'Code' must be a valid currency code.");
    }

    [Test]
    [TestCaseSource(typeof(TestHelper), nameof(TestHelper.GetPredefinedCurrencyCodes))]
    public void Code_IsPredefined_Error(string code)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Code = code;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateCurrencyCommand.Code),
                "NotPredefinedCurrencyCodeValidator",
                "'Code' must not designate a pre-defined currency code.");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(
        "A Very Long Name which a currency should never have Yeah too long should be shorter",
        Description = "Too long")]
    [TestCase(" Andy ", Description = "Starts with spaces")]
    [TestCase("Andy ", Description = "Ends with spaces")]
    public void Name_IsInvalid_Error(string name)
    {
        // Arrange
        var command = this.CreateInstance();
        command.Name = name;

        // Act
        var validationResult = this.ValidateInstance(command);

        // Assert
        validationResult.ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(UpdateCurrencyCommand.Name),
                "FullNameValidator",
                "'Name' must be a valid full name.");
    }

    protected override UpdateCurrencyCommand CreateInstance()
    {
        return new UpdateCurrencyCommand
        {
            Code = "ZIK",
            Name = "Fictional Zik currency",
        };
    }
}