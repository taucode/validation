using FluentValidation;
using NUnit.Framework;
using System;
using System.Linq;
using TauCode.Utils.Extensions;

namespace TauCode.Validation.Test
{
    [TestFixture]
    public class ExactDateValidatorTest
    {
        private static DateTime? MinDate;
        private static DateTime? MaxDate;

        public class Dto
        {
            public DateTime TheDate { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheDate)
                    .ExactDate(MinDate, MaxDate);
            }
        }

        [Test]
        [TestCase("2010-06-06")]
        [TestCase("2011-01-01")]
        public void Validate_MinDateProvidedValueIsValid_Ok(string dateString)
        {
            // Arrange
            MinDate = "2010-06-06".ToExactDate();
            MaxDate = null;

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = dateString.ToExactDate(),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("2010-04-03T19:22:41", Description = "Not exact")]
        [TestCase("2010-06-05", Description = "Less than MinDate")]
        public void Validate_MinDateProvidedValueIsInvalid_Error(string dateString)
        {
            // Arrange
            MinDate = "2010-06-06".ToExactDate();
            MaxDate = null;

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            var result = validator.Validate(dto);
            
            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("ExactDateValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Date' must be an exact date not less than 2010-06-06."));
        }

        [Test]
        [TestCase("2010-06-06")]
        [TestCase("2010-06-05")]
        public void Validate_MaxDateProvidedValueIsValid_Ok(string dateString)
        {
            // Arrange
            MinDate = null;
            MaxDate = "2010-06-06".ToExactDate();

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = dateString.ToExactDate(),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);

        }

        [Test]
        [TestCase("2010-04-03T19:22:41", Description = "Not exact")]
        [TestCase("2010-06-07", Description = "Greater than MaxDate")]
        public void Validate_MaxDateProvidedValueIsInvalid_Error(string dateString)
        {
            // Arrange
            MinDate = null;
            MaxDate = "2010-06-06".ToExactDate();

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("ExactDateValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Date' must be an exact date not greater than 2010-06-06."));
        }

        [Test]
        [TestCase("2010-06-06")]
        [TestCase("2010-06-07")]
        [TestCase("2010-08-07")]
        [TestCase("2010-08-08")]
        public void Validate_MinDateAndMaxDateProvidedValueIsValid_Ok(string dateString)
        {
            // Arrange
            MinDate = "2010-06-06".ToExactDate();
            MaxDate = "2010-08-08".ToExactDate();

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("2010-07-03T19:22:41", Description = "Not exact")]
        [TestCase("2010-06-05", Description = "Less than MinDate")]
        [TestCase("2010-08-09", Description = "Greater than MaxDate")]
        public void Validate_MinDateAndMaxDateProvidedValueIsInvalid_Error(string dateString)
        {
            // Arrange
            MinDate = "2010-06-06".ToExactDate();
            MaxDate = "2010-08-08".ToExactDate();

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("ExactDateValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Date' must be an exact date within range 2010-06-06..2010-08-08."));
        }

        [Test]
        [TestCase("2010-07-03T19:22:41", Description = "Not exact")]
        [TestCase("2010-06-05", Description = "Less than MinDate")]
        [TestCase("2010-08-09", Description = "Greater than MaxDate")]
        public void Validate_MinDateAndMaxDateProvidedAndEqualValueIsInvalid_Error(string dateString)
        {
            // Arrange
            MinDate = "2010-06-06".ToExactDate();
            MaxDate = "2010-06-06".ToExactDate();

            var validator = new DtoValidator();

            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("ExactDateValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Date' must be an exact date equal to 2010-06-06."));
        }

        [Test]
        [TestCase("DateTime.MinValue")]
        [TestCase("DateTime.MaxValue")]
        [TestCase("2010-08-09", Description = "Some value")]
        public void Validate_NoDatesProvidedValueIsValid_Ok(string dateString)
        {
            // Arrange
            MinDate = null;
            MaxDate = null;

            var validator = new DtoValidator();

            DateTime date;

            if (dateString == "DateTime.MinValue")
            {
                date = DateTime.MinValue;
            }
            else if (dateString == "DateTime.MaxValue")
            {
                date = DateTime.MaxValue.Date;
            }
            else
            {
                date = DateTime.Parse(dateString);
            }

            var dto = new Dto
            {
                TheDate = date,
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("2010-07-03T19:22:41", Description = "Not exact")]
        public void Validate_NoDatesProvidedValueIsInvalid_Error(string dateString)
        {
            // Arrange
            MinDate = null;
            MaxDate = null;

            var validator = new DtoValidator();

            // Act
            var dto = new Dto
            {
                TheDate = DateTime.Parse(dateString),
            };

            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("ExactDateValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Date' must be an exact date."));
        }

        [Test]
        public void Constructor_MinDateGreaterThanMaxDate_ThrowsArgumentException()
        {
            // Arrange
            var minDate = "2019-10-10".ToExactDate();
            var maxDate = "2019-10-09".ToExactDate();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new ExactDateValidator(minDate, maxDate));

            Assert.That(ex.Message, Does.StartWith("When both 'minDate' and 'maxDate' are provided, they must be sequential."));
            Assert.That(ex.ParamName, Is.EqualTo("maxDate"));
        }

        [Test]
        public void Constructor_MinDateNotExact_ThrowsArgumentException()
        {
            // Arrange
            var minDate = "2010-10-10".ToExactDate().AddHours(14.88);
            var maxDate = "2019-10-09".ToExactDate();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new ExactDateValidator(minDate, maxDate));

            Assert.That(ex.Message, Does.StartWith("When 'minDate' is provided, it must represent an exact date."));
            Assert.That(ex.ParamName, Is.EqualTo("minDate"));
        }

        [Test]
        public void Constructor_MaxDateNotExact_ThrowsArgumentException()
        {
            // Arrange
            var minDate = "2010-10-10".ToExactDate();
            var maxDate = "2019-10-09".ToExactDate().AddHours(14.88);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new ExactDateValidator(minDate, maxDate));

            Assert.That(ex.Message, Does.StartWith("When 'maxDate' is provided, it must represent an exact date."));
            Assert.That(ex.ParamName, Is.EqualTo("maxDate"));
        }
    }
}
