using NUnit.Framework;
using TauCode.Validation.Tests.Core;
using TauCode.Validation.Tests.Core.Features.SystemWatchers.GetSystemWatcher;

namespace TauCode.Validation.Tests.SystemWatchers;

[TestFixture]
public class GetSystemWatcherQueryValidatorTests : ValidatorTestBase<
    GetSystemWatcherQuery,
    GetSystemWatcherQueryValidator>
{
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
        null,
        false,
        Description = "No params provided"
    )]
    [TestCase(
        // 1
        null,
        null,
        DataConstants.SystemWatcher.DefaultSystemWatcherCode,
        true,
        Description = "Exactly one param provided"
    )]
    [TestCase(
        // 2
        null,
        DataConstants.SystemWatcher.DefaultSystemWatcherGuidString,
        null,
        true,
        Description = "Exactly one param provided"
    )]
    [TestCase(
        // 3
        null,
        DataConstants.SystemWatcher.DefaultSystemWatcherGuidString,
        DataConstants.SystemWatcher.DefaultSystemWatcherCode,
        false,
        Description = "More than one param provided"
    )]
    [TestCase(
        // 4
        DataConstants.SystemWatcher.DefaultSystemWatcherId,
        null,
        null,
        true,
        Description = "Exactly one param provided"
    )]
    [TestCase(
        // 5
        DataConstants.SystemWatcher.DefaultSystemWatcherId,
        null,
        DataConstants.SystemWatcher.DefaultSystemWatcherCode,
        false,
        Description = "More than one param provided"
    )]
    [TestCase(
        // 6
        DataConstants.SystemWatcher.DefaultSystemWatcherId,
        DataConstants.SystemWatcher.DefaultSystemWatcherGuidString,
        null,
        false,
        Description = "More than one param provided"
    )]
    [TestCase(
        // 7
        DataConstants.SystemWatcher.DefaultSystemWatcherId,
        DataConstants.SystemWatcher.DefaultSystemWatcherGuidString,
        DataConstants.SystemWatcher.DefaultSystemWatcherCode,
        false,
        Description = "More than one param provided"
    )]
    public void Query_PropertiesProvided_ReturnsProperResponse(
        long? id,
        string guidString,
        string code,
        bool shouldBeOk)
    {
        // Arrange
        var guid = guidString.ToNullableGuid();
        var query = new GetSystemWatcherQuery
        {
            Id = id,
            Guid = guid,
            Code = code,
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
                    nameof(GetSystemWatcherQueryValidator),
                    "Exactly one property of the following should be provided: 'Id', 'Guid', 'Code'.");
        }
    }

    [Test]
    [TestCase(0L)]
    [TestCase(-1L)]
    public void Id_IsInvalid_Error(long id)
    {
        // Arrange
        var query = this.CreateInstance();
        query.Id = id;
        query.Guid = null;
        query.Code = null;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetSystemWatcherQuery.Id),
                "LongIdValidator",
                "'Id' must be a valid long Id. This property is optional and can be null.");
    }

    [Test]
    public void Guid_IsEmpty_Error()
    {
        // Arrange
        var query = this.CreateInstance();
        query.Id = null;
        query.Guid = Guid.Empty;
        query.Code = null;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetSystemWatcherQuery.Guid),
                "NotEmptyValidator",
                "'Guid' must not be empty. This property is optional and can be null.");
    }

    [Test]
    [TestCase(" acme")]
    [TestCase("Acme")]
    [TestCase(
        "too-long-system-watcher-code-you-should-not-really-use-long-names-like-this-please-try-using-shorter-names")]
    public void Code_IsInvalid_Error(string code)
    {
        // Arrange
        var query = this.CreateInstance();
        query.Id = null;
        query.Guid = null;
        query.Code = code;

        // Act
        var validationResult = this.ValidateInstance(query);

        // Assert
        validationResult
            .ShouldBeInvalid(1)
            .ShouldHaveError(
                0,
                nameof(GetSystemWatcherQuery.Code),
                "SystemWatcherCodeValidator",
                "'Code' must be a valid System Watcher code. This property is optional and can be null.");
    }

    protected override GetSystemWatcherQuery CreateInstance()
    {
        return new GetSystemWatcherQuery
        {
            Id = null,
            Guid = null,
            Code = "my-acme-1",
        };
    }
}