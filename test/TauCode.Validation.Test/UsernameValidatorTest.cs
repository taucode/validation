using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Test
{
    [TestFixture]
    public class UsernameValidatorTest
    {
        public class Dto
        {
            public string MyUsername { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.MyUsername)
                    .Username(2, 10);
            }
        }

        [Test]
        [TestCase("ak")]
        [TestCase("admin")]
        [TestCase("1and1")]
        [TestCase("1and1_go")]
        public void Validate_ValidUsername_Ok(string username)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                MyUsername = username,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("a")]
        [TestCase("tooooooooooooo_looooooooooong")]
        [TestCase("admin-2")]
        [TestCase(" aka")]
        [TestCase("оля")]
        public void Validate_InvalidUsername_Error(string username)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                MyUsername = username,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("UsernameValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'My Username' must be a valid username."));
        }
    }
}
