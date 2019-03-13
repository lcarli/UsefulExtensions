using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsefulExtensions
{
    public static class IEnumerableExtensions
    {
        //customers.ForEach<Customer>(t=> t.Enabled = true);
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
            return collection;
        }

        public static IEnumerable<T> InterleaveLists<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            using (IEnumerator<T>
                enumerator1 = first.GetEnumerator(),
                enumerator2 = second.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    yield return enumerator1.Current;
                    if (enumerator2.MoveNext())
                        yield return enumerator2.Current;
                }
            }
        }
    }
}
