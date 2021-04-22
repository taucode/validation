using FluentValidation;
using System;
using TauCode.Validation.Tests.Core.Validators;

namespace TauCode.Validation.Tests.Core
{
    internal static class CoreValidationExtensions
    {
        internal static IRuleBuilderOptions<T, string> SystemWatcherCode<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SystemWatcherCodeValidator<T>());
        }

        internal static IRuleBuilderOptions<T, DateTimeOffset> QuoteDate<T>(
            this IRuleBuilder<T, DateTimeOffset> ruleBuilder)
        {
            throw new NotImplementedException();
            //return ruleBuilder.SetValidator(new QuoteDateValidator(false));
        }

        internal static IRuleBuilderOptions<T, string> NotPredefinedCurrencyCode<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotPredefinedCurrencyCodeValidator<T>());
        }

        internal static IRuleBuilderOptions<T, long> NotPredefinedCurrencyId<T>(
            this IRuleBuilder<T, long> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotPredefinedCurrencyIdValidator<T>());
        }

        internal static IRuleBuilderOptions<T, decimal> QuoteRate<T>(
            this IRuleBuilder<T, decimal> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new QuoteRateValidator<T>());
        }


    }
}
