using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Tests
{
    [TestFixture]
    public class FullNameValidatorTest
    {
        public class Dto
        {
            public string TheName { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheName)
                    .FullName(2, 10);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("ak")]
        [TestCase("Оля")]
        [TestCase("er ein årl")]
        public void Validate_ValidName_Ok(string name)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheName = name,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase(" admin-2")]
        [TestCase("оля ")]
        [TestCase("Очень длинное имя")]
        public void Validate_InvalidName_Error(string name)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheName = name,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("FullNameValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Name' must be a valid full name."));
        }
    }
}
