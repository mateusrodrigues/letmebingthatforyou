using System;
using System.Collections.Generic;
using System.Linq;

namespace Lmbtfy.Web.Extensions {
    public static class EnumerableExtensions {
        public static T GetElementOfTheDay<T>(this IEnumerable<T> items, DateTime date) {
            if (items == null) {
                throw new ArgumentNullException("items");
            }

            var index = (int)(date - DateTime.MinValue).TotalDays % items.Count();
            return items.ElementAt(index);
        }
    }
}