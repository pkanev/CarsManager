using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation.Validators;

namespace CarsManager.Application.Common.Validators
{
    public class UniqueValidator<T> : PropertyValidator where T : class
    {
        private readonly IEnumerable<T> items;
        private readonly bool isCaseSensitive;
        private readonly bool needsTrimming;

        public UniqueValidator(IEnumerable<T> items, bool isCaseSensitive = true, bool needsTrimming = false)
        {
            this.items = items;
            this.isCaseSensitive = isCaseSensitive;
            this.needsTrimming = needsTrimming;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var editedItem = context.InstanceToValidate as T;
            var newValue = context.PropertyValue as string;
            if (needsTrimming)
                newValue = newValue.Trim();

            var property = typeof(T).GetTypeInfo().GetDeclaredProperty(context.PropertyName);
            return !items.Any(item =>
            {
                var isSameItem = item.Equals(editedItem);
                var hasEqualProperty = isCaseSensitive
                    ? property.GetValue(item).ToString() == newValue
                    : property.GetValue(item).ToString().ToLower() == newValue.ToLower();

                return !isSameItem && hasEqualProperty;
            });
        }
    }
}
