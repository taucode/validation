using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Tests
{
    [TestFixture]
    public class CurrencyCodeValidatorTest
    {
        public class Dto
        {
            public string CurrencyCode { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.CurrencyCode)
                    .CurrencyCode();
            }
        }

        [Test]
        [TestCase("USD")]
        [TestCase("UAH")]
        [TestCase("WAT", Description = "Fictional code, but pattern is valid")]
        public void Validate_CurrencyCodeIsValid_Ok(string code)
        {
            // Arrange
            var validator = new DtoValidator();
            var dto = new Dto
            {
                CurrencyCode = code,
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("", Description = "Empty")]
        [TestCase("RB", Description = "Not 3 symbols")]
        [TestCase("EURO", Description = "Not 3 symbols")]
        [TestCase("USd", Description = "Not all upper-case")]
        [TestCase("RB.", Description = "Not all letters")]
        public void Validate_CurrencyCodeIsInvalid_Error(string code)
        {
            // Arrange
            var validator = new DtoValidator();
            var dto = new Dto
            {
                CurrencyCode = code,
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("CurrencyCodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'Currency Code' must be a valid currency code."));
        }
    }
}
