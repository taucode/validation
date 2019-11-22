using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Tests
{
    [TestFixture]
    public class WebCodeValidatorTest
    {
        public class Dto
        {
            public string TheWebCode { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheWebCode)
                    .WebCode(2, 20);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("ak")]
        [TestCase("some-note")]
        [TestCase("1st-good-girl90")]
        public void Validate_ValidWebCode_Ok(string webCode)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheWebCode = webCode,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("a-", Description = "Ends with separator")]
        [TestCase("a--b", Description = "Two separators in a row")]
        [TestCase("-a-b", Description = "Starts with separator")]
        [TestCase("tooooooooooooo-looooooooooong")]
        [TestCase("оля")]
        [TestCase("tag1,tag2")]
        public void Validate_InvalidWebCode_Error(string webCode)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheWebCode = webCode,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("WebCodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Web Code' must be a valid web code."));
        }

    }
}
