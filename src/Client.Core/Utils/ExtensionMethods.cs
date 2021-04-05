using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Client.Core.Models;

namespace Client.Core.Utils
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new ObservableCollection<T>();
            if (enumerable == null)
                return collection;

            foreach (var current in enumerable)
                collection.Add(current);

            return collection;
        }

        public static DateTime AddValidityPeriod(
            this DateTime date,
            ValidityPeriod validityPeriod) => date
                .AddDays(validityPeriod.Days)
                .AddMonths(validityPeriod.Months)
                .AddYears(validityPeriod.Years)
                .AddDays(-1);
    }
}
