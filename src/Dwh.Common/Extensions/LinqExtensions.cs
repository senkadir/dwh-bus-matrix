using System.Collections.Generic;
using System.Linq;

namespace Dwh.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
