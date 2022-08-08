using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.Watchers.GetWatcher;

namespace TauCode.Validation.Tests.Watchers;

[TestFixture]
public class GetWatcherQueryValidatorTests : ValidatorTestBase<GetWatcherQuery, GetWatcherQueryValidator>
{
    private const string GuidString = "ae04ebdd-0f40-469b-a1c0-a5cca85d522c";

    [SetUp]
    public void SetUp()
    {
        this.SetUpImpl();
    }

    [Test]
    [TestCase(
        // 0
        null,
        null,
        false,
        Description = "No params provided"
    )]
    [TestCase(
        // 1
        null,
        GuidString,
        true,
        Description = "Exactly one param provided"
    )]
    [TestCase(
        // 2
        2L,
        null,
        true,
        Description = "Exactly one param provided"
    )]
    [TestCase(
        // 3
        2L,
        GuidString,
        false,
        Description = "Both params provided"
    )]
    public void Query_PropertiesProvided_ReturnsProperResponse(
        long? id,
        string guidString,
        bool shouldBeOk)
    {
        // Arrange
        var guid = guidString.ToNullableGuid();
        var query = new GetWatcherQuery
        {
            Id = id,
            Guid = guid,
        };

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        if (shouldBeOk)
        {
            validationResult.ShouldBeValid();
        }
        else
        {
            validationResult
                .ShouldBeInvalid(1)
                .ShouldHaveError(
                    0,
                    "Query",
                    nameof(GetWatcherQueryValidator),
                    "Exactly one property of the following should be provided: 'Id', 'Guid'.");
        }
    }

    [Test]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void Id_IsBad_Error(long badId)
    {
        // Arrange
        var query = new GetWatcherQuery
        {
            Id = badId,
        };

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherQuery.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id. This property is optional and can be null.");
    }

    [Test]
    public void Id_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var query = new GetWatcherQuery
        {
            Id = DataConstants.SystemWatcher.DefaultSystemWatcherId,
        };

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherQuery.Id),
                "NotEqualValidator",
                "'Id' must not be equal to '1'.");
    }

    [Test]
    public void Guid_IsEmpty_Error()
    {
        // Arrange
        var query = new GetWatcherQuery
        {
            Guid = Guid.Empty,
        };

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherQuery.Guid),
                "NotEmptyValidator",
                "'Guid' must not be empty. This property is optional and can be null.");
    }

    [Test]
    public void Guid_IsOfDefaultSystemWatcher_Error()
    {
        // Arrange
        var query = new GetWatcherQuery
        {
            Guid = DataConstants.SystemWatcher.DefaultSystemWatcherGuid,
        };

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetWatcherQuery.Guid),
                "NotEqualValidator",
                $"'Guid' must not be equal to '{DataConstants.SystemWatcher.DefaultSystemWatcherGuid}'.");
    }


    protected override GetWatcherQuery CreateInstance()
    {
        return new GetWatcherQuery();
    }
}