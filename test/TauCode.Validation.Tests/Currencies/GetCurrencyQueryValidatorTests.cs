using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Currencies.GetCurrency;

namespace TauCode.Validation.Tests.Currencies
{
    [TestFixture]
    public class GetCurrencyQueryValidatorTests : ValidatorTestBase<GetCurrencyQuery, GetCurrencyQueryValidator>
    {
        [SetUp]
        public void SetUp()
        {
            this.SetUpImpl();
        }

        [Test]
        [TestCase(null, DataConstants.Currency.UsdCode)]
        [TestCase(DataConstants.Currency.UsdId, null)]
        public void Query_ValidProperties_Ok(long? id, string code)
        {
            // Arrange
            var query = this.CreateInstance();

            query.Id = id;
            query.Code = code;

            // Act
            var validationResult = this.ValidateInstance(query);

            // Assert
            validationResult.ShouldBeValid();
        }

        [Test]
        [TestCase(null, null)]
        [TestCase(DataConstants.Currency.UsdId, DataConstants.Currency.UsdCode)]
        public void Query_BothOrNoneProvided_Error(long? id, string code)
        {
            // Arrange
            var query = this.CreateInstance();

            query.Id = id;
            query.Code = code;

            // Act
            var validationResult = this.ValidateInstance(query);

            // Assert
            validationResult
                .ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    "Query",
                    nameof(GetCurrencyQueryValidator),
                    "Exactly one property of the following should be provided: 'Id', 'Code'.");
        }

        [Test]
        [TestCase("")]
        [TestCase("RB", Description = "Not 3 symbols")]
        [TestCase("EURO", Description = "Not 3 symbols")]
        [TestCase("USd", Description = "Not all upper-case")]
        [TestCase("RB.", Description = "Not all letters")]
        public void Code_IsInvalid_Error(string code)
        {
            // Arrange
            var query = this.CreateInstance();
            query.Id = null;
            query.Code = code;

            // Act
            var validationResult = this.ValidateInstance(query);

            // Assert
            validationResult
                .ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    nameof(GetCurrencyQuery.Code),
                    "CurrencyCodeValidator",
                    "'Code' must be a valid currency code. This property is optional and can be null.");
        }

        [Test]
        [TestCase(0L)]
        [TestCase(-1L)]
        public void Id_IsInvalid_Error(long badId)
        {
            // Arrange
            var query = this.CreateInstance();
            query.Id = badId;
            query.Code = null;

            // Act
            var validationResult = this.ValidateInstance(query);

            // Assert
            validationResult
                .ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    nameof(GetCurrencyQuery.Id),
                    "LongIdValidator",
                    "'Id' must be a valid long Id. This property is optional and can be null.");
        }

        protected override GetCurrencyQuery CreateInstance()
        {
            return new GetCurrencyQuery
            {
                Id = null,
                Code = "USD",
            };
        }
    }
}
