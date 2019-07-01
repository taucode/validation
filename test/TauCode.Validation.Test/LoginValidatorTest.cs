using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace TauCode.Validation.Test
{
    [TestFixture]
    public class LoginValidatorTest
    {
        public class Dto
        {
            public string TheLogin { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheLogin)
                    .Login(2, 10);
            }
        }

        [Test]
        [TestCase("ak")]
        [TestCase("admin")]
        [TestCase("1and1")]
        [TestCase("1and1_go")]
        public void Validate_ValidLogin_Ok(string login)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheLogin = login,
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
        public void Validate_InvalidLogin_Error(string login)
        {
            // Arrange
            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheLogin = login,
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("LoginValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Login' must be a valid login."));
        }
    }
}
