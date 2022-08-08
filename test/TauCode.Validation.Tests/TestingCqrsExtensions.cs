using FluentValidation.Results;
using NUnit.Framework;
using System.Globalization;
using System.Text;



// todo rename. why the hell 'cqrs'?
// todo clean
// todo notimpl - get rid of
namespace TauCode.Validation.Tests;

public static class TestingCqrsExtensions
{
    public static ValidationResult ShouldBeValid(this ValidationResult validationResult)
    {
        Assert.That(validationResult.IsValid, Is.True);
        return validationResult;
    }

    public static ValidationResult ShouldBeInvalid(this ValidationResult validationResult, int expectedErrorCount)
    {
        Assert.That(validationResult.IsValid, Is.False);
        Assert.That(validationResult.Errors, Has.Count.EqualTo(expectedErrorCount));

        return validationResult;
    }

    public static ValidationResult ShouldHaveError(
        this ValidationResult validationResult,
        int errorIndex,
        string propertyName,
        string expectedErrorCode,
        string expectedErrorMessage)
    {
        Assert.That(validationResult.Errors[errorIndex].PropertyName, Is.EqualTo(propertyName));
        Assert.That(validationResult.Errors[errorIndex].ErrorCode, Is.EqualTo(expectedErrorCode));
        Assert.That(validationResult.Errors[errorIndex].ErrorMessage, Is.EqualTo(expectedErrorMessage));

        return validationResult;
    }

    //public static TId ToId<TId>(this string id) where TId : IdBase
    //{
    //    if (id == null)
    //    {
    //        return null;
    //    }

    //    return (TId)typeof(TId)
    //        .GetConstructor(new[] { typeof(string) })
    //        .Invoke(new object[] { id });
    //}

    public static string SubstituteUsername(this string username)
    {
        if (username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (username.StartsWith("$"))
        {
            var len = username.Substring(1).ToInt32();
            var sb = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                sb.Append("a");
            }

            username = sb.ToString();
        }

        return username;
    }

    public static string SubstitutePasswordHash(this string passwordHash)
    {
        if (passwordHash == null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        if (passwordHash.StartsWith("$"))
        {
            var len = passwordHash.Substring(1).ToInt32();
            var sb = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                sb.Append("x");
            }

            passwordHash = sb.ToString();
        }

        return passwordHash;
    }

    public static string SubstituteEmail(this string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (email.StartsWith("$"))
        {
            var len = email.Substring(1).ToInt32();
            var sb = new StringBuilder();

            var times = len - "@m.net".Length;

            for (var i = 0; i < times; i++)
            {
                sb.Append("a");
            }

            sb.Append("@m.net");

            email = sb.ToString();
        }

        return email;
    }

    public static string SubstituteDomainName(this string domainName)
    {
        if (domainName == null)
        {
            throw new ArgumentNullException(nameof(domainName));
        }

        if (domainName.StartsWith("$"))
        {
            var len = domainName.Substring(1).ToInt32();
            var sb = new StringBuilder();

            var times = len - "m.net".Length;

            for (var i = 0; i < times; i++)
            {
                sb.Append("a");
            }

            sb.Append("m.net");

            domainName = sb.ToString();
        }

        return domainName;
    }

    public static string SubstituteCode(this string code, bool lowerCase = true)
    {
        if (code == null)
        {
            throw new ArgumentNullException(nameof(code));
        }

        var c = lowerCase ? 'a' : 'A';

        if (code.StartsWith("$"))
        {
            var len = code.Substring(1).ToInt32();
            var sb = new StringBuilder();

            var times = len;

            for (var i = 0; i < times; i++)
            {
                sb.Append(c);
            }

            code = sb.ToString();
        }

        return code;
    }

    public static string SubstituteName(this string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (name.StartsWith("$"))
        {
            var len = name.Substring(1).ToInt32();
            var sb = new StringBuilder();

            var times = len;

            for (var i = 0; i < times; i++)
            {
                var c = 'a';
                if (i == 0)
                {
                    c = 'A';
                }

                sb.Append(c);
            }

            name = sb.ToString();
        }

        return name;
    }

    internal static int ToInt32(this string s) => int.Parse(s, CultureInfo.InvariantCulture);
}