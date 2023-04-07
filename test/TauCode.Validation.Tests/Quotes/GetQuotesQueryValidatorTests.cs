using NUnit.Framework;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Quotes.GetQuotes;

namespace TauCode.Validation.Tests.Quotes;

[TestFixture]
public class GetQuotesQueryValidatorTests : ValidatorTestBase<
    GetQuotesQuery,
    GetQuotesQueryValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
    }

    protected override GetQuotesQuery CreateInstance()
    {
        return new GetQuotesQuery
        {
            WatcherId = 100101L, // some real-looking watcher id
            Date = "2019-03-07Z".ToUtcDateOffset(),
        };
    }

    [Test]
    [TestCase(null)]
    [TestCase("2019-02-02Z")]
    public void Query_IsValid_RunsOk(string dateString)
    {
        // Arrange
        var query = this.CreateInstance();
        query.Date = dateString.ToNullableUtcDateOffset();

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult.ShouldBeValid();
    }

    [Test]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void WatcherId_IsBad_Error(long badId)
    {
        // Arrange
        var query = this.CreateInstance();
        query.WatcherId = badId;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetQuotesQuery.WatcherId),
                "LongIdValidator",
                "'Watcher Id' must be a valid Id.");
    }

    [Test]
    [TestCase("1899-01-01Z", Description = "Before beginning of the time")]
    [TestCase("3000-01-01Z", Description = "After end of the time")]
    [TestCase("2019-02-01 02:00:00 +02:00", Description = "UTC date is exact but shift is not a zero timespan")]
    [TestCase("2019-02-01 02:00:00 +00:00", Description = "Time of day is not zero")]
    public void Date_IsBad_Error(string badDateString)
    {
        // Arrange
        var query = this.CreateInstance();
        query.Date = DateTimeOffset.Parse(badDateString);

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetQuotesQuery.Date),
                "QuoteDateValidator",
                "'Date' must be an exact date within range 1900-01-01..2999-12-31. This property is optional and can be null.");
    }

}