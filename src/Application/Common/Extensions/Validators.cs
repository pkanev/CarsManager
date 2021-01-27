using System.Collections.Generic;
using CarsManager.Application.Common.Validators;
using FluentValidation;

namespace CarsManager.Application.Common.Extensions
{
    public static class Validators
    {
        public static IRuleBuilderOptions<TItem, TProperty> IsUnique<TItem, TProperty>(
            this IRuleBuilder<TItem, TProperty> ruleBuilder,
            IEnumerable<TItem> items, bool isCaseSensitive = true, bool needsTrimming = false) where TItem : class
        {
            return ruleBuilder.SetValidator(new UniqueValidator<TItem>(items, isCaseSensitive, needsTrimming));
        }
    }

}
