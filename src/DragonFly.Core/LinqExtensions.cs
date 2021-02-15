using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Core
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach(T item in items)
            {
                action(item);
            }

            return items;
        }

    }
}
