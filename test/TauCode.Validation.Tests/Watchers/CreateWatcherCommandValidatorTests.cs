using NUnit.Framework;
using System;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.CreateWatcher;

namespace TauCode.Validation.Tests.Watchers
{
    [TestFixture]
    public class CreateWatcherCommandValidatorTests : ValidatorTestBase<CreateWatcherCommand, CreateWatcherCommandValidator>
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
                .ShouldHaveError(0,
                    nameof(CreateWatcherCommand.Guid),
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
                    nameof(CreateWatcherCommand.Guid),
                    "NotEqualValidator",
                    $"'Guid' must not be equal to '{DataConstants.SystemWatcher.DefaultSystemWatcherGuid}'.");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("RB", Description = "Not 3 symbols")]
        [TestCase("EURO", Description = "Not 3 symbols")]
        [TestCase("USd", Description = "Not all upper-case")]
        [TestCase("RB.", Description = "Not all letters")]
        public void BasicCurrencyCode_IsBad_Error(string badCurrencyCode)
        {
            // Arrange
            var command = this.CreateInstance();

            command.BasicCurrencyCode = badCurrencyCode;

            // Act
            var validationResult = this.ValidateInstance(command);

            // Assert
            validationResult
                .ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    nameof(CreateWatcherCommand.BasicCurrencyCode),
                    "CurrencyCodeValidator",
                    "'Basic Currency Code' must be a valid currency code.");
        }

        protected override CreateWatcherCommand CreateInstance()
        {
            return new CreateWatcherCommand
            {
                Guid = new Guid("aeded665-774d-4819-8c9b-7b116dfc9532"),
                BasicCurrencyCode = DataConstants.Currency.UsdCode,
            };
        }
    }
}
