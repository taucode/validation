using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Tests
{
    [TestFixture]
    public class LowercaseValidatorTests
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
                    .LowercaseCode(2, 20);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("ak")]
        [TestCase("some_note")]
        [TestCase("1st_good_girl90")]
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
        [TestCase("a")]
        [TestCase("a_", Description = "Ends with separator")]
        [TestCase("a__b", Description = "Two separators in a row")]
        [TestCase("_a_b", Description = "Starts with separator")]
        [TestCase("tooooooooooooo_looooooooooong")]
        [TestCase("оля")]
        [TestCase("Some_note")]
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
            Assert.That(error.ErrorCode, Is.EqualTo("LowercaseCodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Code' must be a valid lowercase code."));
        }
    }
}
