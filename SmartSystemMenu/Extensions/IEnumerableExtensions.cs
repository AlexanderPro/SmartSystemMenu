using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSystemMenu.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f) => e.SelectMany(c => f(c).Flatten(f)).Concat(e);
    }
}
