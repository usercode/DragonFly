﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Helpers
{
    static class ListSortExtensions
    {
        public static void MoveUp<T>(this IList<T> sourceList, T item)
        {
            int pos = sourceList.IndexOf(item);

            if (pos == 0)
            {
                return;
            }

            sourceList.RemoveAt(pos);
            sourceList.Insert(pos - 1, item);
        }

        public static void MoveDown<T>(this IList<T> sourceList, T item)
        {
            int pos = sourceList.IndexOf(item);

            if (pos == sourceList.Count - 1)
            {
                return;
            }

            sourceList.RemoveAt(pos);
            sourceList.Insert(pos + 1, item);
        }
    }
}
