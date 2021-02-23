using System;
using System.Collections.Generic;

namespace FileapiCli.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            return obj == null || String.IsNullOrWhiteSpace(obj.ToString());
        }
        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>
        (this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> projection)
        {
            using var iterator = source.GetEnumerator();
            if (!iterator.MoveNext())
            {
                yield break;
            }
            TSource previous = iterator.Current;
            while (iterator.MoveNext())
            {
                yield return projection(previous, iterator.Current);
                previous = iterator.Current;
            }
        }
    }

}
