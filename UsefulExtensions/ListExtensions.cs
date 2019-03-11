using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsefulExtensions
{
    public static class ListExtensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }
    }
}
