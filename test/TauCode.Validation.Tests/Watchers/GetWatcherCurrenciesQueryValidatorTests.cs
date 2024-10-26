using NUnit.Framework;
using TauCode.Extensions;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.GetWatcherCurrencies;

namespace TauCode.Validation.Tests.Watchers;

[TestFixture]
public class GetWatcherCurrenciesQueryValidatorTests : ValidatorTestBase<
    GetWatcherCurrenciesQuery,
    GetWatcherCurrenciesQueryValidator>
{
    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
    }

    [Test]
    [TestCase(null)]
    [TestCase("2019-03-03Z")]
    public void Query_IsValid_RunsOk(string? dateString)
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
                nameof(GetWatcherCurrenciesQuery.WatcherId),
                "LongIdValidator",
                "'Watcher Id' must be a valid Id.");
    }

    [Test]
    public void WatcherId_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var query = this.CreateInstance();
        query.WatcherId = DataConstants.SystemWatcher.DefaultSystemWatcherId;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherCurrenciesQuery.WatcherId),
                "NotEqualValidator",
                "'Watcher Id' must not be equal to '1'.");
    }

    [Test]
    [TestCase("1899-01-01Z", Description = "Before beginning of the time")]
    [TestCase("3000-01-01Z", Description = "After end of the time")]
    [TestCase("2019-02-01 02:00:00 +02:00", Description = "UTC date is exact but shift is not a zero timespan")]
    [TestCase("2019-02-01 02:00:00 +00:00", Description = "Time of day is not zero")]
    public void Date_IsInvalid_Error(string dateString)
    {
        // Arrange
        var date = DateTimeOffset.Parse(dateString);
        var query = this.CreateInstance();
        query.Date = date;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherCurrenciesQuery.Date),
                "QuoteDateValidator",
                "'Date' must be an exact date within range 1900-01-01..2999-12-31. This property is optional and can be null.");
    }

    protected override GetWatcherCurrenciesQuery CreateInstance()
    {
        return new GetWatcherCurrenciesQuery
        {
            WatcherId = 2L,
            Date = "2019-01-02Z".ToUtcDateOffset(),
        };
    }
}