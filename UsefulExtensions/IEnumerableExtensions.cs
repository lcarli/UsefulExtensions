using System;
using System.Collections.Generic;
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
    }
}
