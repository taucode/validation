using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Currencies.CreateCurrency;

namespace TauCode.Validation.Tests.Currencies;

[TestFixture]
public class CreateCurrencyCommandValidatorTests : ValidatorTestBase<
    CreateCurrencyCommand,
    CreateCurrencyCommandValidator>
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
    [TestCase(null)]
    [TestCase("")]
    [TestCase("RB", Description = "Not 3 symbols")]
    [TestCase("EURO", Description = "Not 3 symbols")]
    [TestCase("USd", Description = "Not all upper-case")]
    [TestCase("RB.", Description = "Not all letters")]
    public void Code_IsBad_Error(string? code)
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
                "Code",
                /*nameof(CurrencyCodeValidatorLab)*/"CurrencyCodeValidator",
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
                nameof(CreateCurrencyCommand.Code),
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
    public void Name_IsInvalid_Error(string? name)
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
                nameof(CreateCurrencyCommand.Name),
                "FullNameValidator",
                "'Name' must be a valid full name.");
    }

    protected override CreateCurrencyCommand CreateInstance()
    {
        var command = new CreateCurrencyCommand
        {
            Code = "ZZZ",
            Name = "Fictional currency",
        };

        return command;
    }
}