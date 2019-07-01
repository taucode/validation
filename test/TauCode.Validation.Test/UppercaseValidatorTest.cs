using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Test
{
    [TestFixture]
    public class UppercaseValidatorTest
    {
        public class Dto
        {
            public string TheCode { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheCode)
                    .UppercaseCode(2, 20);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("AK")]
        [TestCase("SOME_NOTE")]
        [TestCase("1ST_GOOD_GIRL90")]
        public void Validate_ValidCode_Ok(string code)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheCode = code,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase("A")]
        [TestCase("A_", Description = "Ends with separator")]
        [TestCase("A__B", Description = "Two separators in a row")]
        [TestCase("_A_B", Description = "Starts with separator")]
        [TestCase("TOOOOOOOOOOOOO_LOOOOOOOOOOONG")]
        [TestCase("ОЛЯ")]
        [TestCase("SOME_NoTE")]
        public void Validate_InvalidCode_Error(string code)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheCode = code,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("UppercaseCodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Code' must be a valid uppercase code."));
        }
    }
}
