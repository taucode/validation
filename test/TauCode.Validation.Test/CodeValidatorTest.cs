using FluentValidation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TauCode.Validation.Test
{
    [TestFixture]
    public class CodeValidatorTest
    {
        private static readonly char[] Alphabet;
        private static readonly char[] Separators = { '-', '_' };
        private static readonly char[] EmptyChars = new char[0];

        private static int MinLength;
        private static int MaxLength;

        static CodeValidatorTest()
        {
            var smallLetters = Enumerable
                .Range('a', 'z' - 'a' + 1)
                .Select(x => (char)x)
                .ToArray();

            var capitalLetters = Enumerable
                .Range('A', 'Z' - 'A' + 1)
                .Select(x => (char)x)
                .ToArray();

            var digits = Enumerable
                .Range('0', '9' - '0' + 1)
                .Select(x => (char)x)
                .ToArray();

            var alphabetList = new List<char>();
            alphabetList.AddRange(smallLetters);
            alphabetList.AddRange(capitalLetters);
            alphabetList.AddRange(digits);

            Alphabet = alphabetList.ToArray();
        }

        public class Dto
        {
            public string TheCode { get; set; }
        }

        public class DtoValidator : AbstractValidator<Dto>
        {
            public DtoValidator()
            {
                this.RuleFor(x => x.TheCode)
                    .Code(MinLength, MaxLength, Alphabet, Alphabet, Separators);
            }
        }

        [Test]
        [TestCase("coda")]
        [TestCase("Coda")]
        [TestCase("Coda_meyer-10")]
        [TestCase("1488_ZIP-FILE-740")]
        public void Validate_CodeIsValid_Ok(string code)
        {
            // Arrange
            MinLength = 2;
            MaxLength = 30;

            var validator = new DtoValidator();
            var dto = new Dto
            {
                TheCode = code,
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        [TestCase("h", Description = "Too short")]
        [TestCase("-coda", Description = "Starts with separator")]
        [TestCase("CodaОля", Description = "Contains chars not from alphabet")]
        [TestCase("Coda_meyer-10-", Description = "Ends with separator")]
        [TestCase("1488_ZIP-FILE--740", Description = "Two consequent separators")]
        [TestCase("tooooooooooooooooooooooooooo-loooooooooooooooooooong", Description = "Two consequent separators")]
        public void Validate_CodeIsInvalidValid_Error(string code)
        {
            // Arrange
            MinLength = 2;
            MaxLength = 30;

            var validator = new DtoValidator();
            var dto = new Dto
            {
                TheCode = code,
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("CodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Code' must be a valid code with length 2..30."));
        }

        [Test]
        public void Validate_MinLengthEqualsMaxLengthCodeIsInvalidValid_Error()
        {
            // Arrange
            MinLength = 2;
            MaxLength = 2;

            var validator = new DtoValidator();
            var dto = new Dto
            {
                TheCode = "wat",
            };

            // Act
            var result = validator.Validate(dto);

            // Assert
            Assert.That(result.IsValid, Is.False);
            var error = result.Errors.Single();
            Assert.That(error.ErrorCode, Is.EqualTo("CodeValidator"));
            Assert.That(error.ErrorMessage, Is.EqualTo("'The Code' must be a valid code with length 2."));
        }

        [Test]
        public void Constructor_InvalidArguments_ThrowsArgumentExceptions()
        {
            // Arrange

            // Act & Assert

            // min length is 0
            var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new CodeValidator(0, 10, Alphabet, Alphabet, Separators));

            // min length is negative
            var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new CodeValidator(-1, 10, Alphabet, Alphabet, Separators));

            // min length is greater than max length
            var ex3 = Assert.Throws<ArgumentException>(() =>
                new CodeValidator(11, 10, Alphabet, Alphabet, Separators));

            // alphabet is null
            var ex4 = Assert.Throws<ArgumentNullException>(() =>
                new CodeValidator(1, 10, null, Alphabet, Separators));

            // alphabet is empty
            var ex5 = Assert.Throws<ArgumentException>(() =>
                new CodeValidator(1, 10, EmptyChars, Alphabet, Separators));

            // starting chars is null
            var ex6 = Assert.Throws<ArgumentNullException>(() =>
                new CodeValidator(1, 10, Alphabet, null, Separators));

            // starting chars is empty
            var ex7 = Assert.Throws<ArgumentException>(() =>
                new CodeValidator(1, 10, Alphabet, EmptyChars, Separators));

            // separators is null
            var ex8 = Assert.Throws<ArgumentNullException>(() =>
                new CodeValidator(1, 10, Alphabet, Alphabet, null));

            // Assert
            Assert.That(ex1.ParamName, Is.EqualTo("minLength"));

            Assert.That(ex2.ParamName, Is.EqualTo("minLength"));

            Assert.That(ex3.Message, Does.StartWith("'maxLength' must be not less than 'minLength'"));
            Assert.That(ex3.ParamName, Is.EqualTo("maxLength"));

            Assert.That(ex4.ParamName, Is.EqualTo("alphabet"));

            Assert.That(ex5.Message, Does.StartWith("'alphabet' cannot be empty."));
            Assert.That(ex5.ParamName, Is.EqualTo("alphabet"));

            Assert.That(ex6.ParamName, Is.EqualTo("startingChars"));

            Assert.That(ex7.Message, Does.StartWith("'startingChars' cannot be empty."));
            Assert.That(ex7.ParamName, Is.EqualTo("startingChars"));

            Assert.That(ex8.ParamName, Is.EqualTo("separators"));
        }
    }
}
