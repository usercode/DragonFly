using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Helpers
{
    /// <summary>
    /// SelectableObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectableObject<T>
    {
        public SelectableObject(bool isSelected, T obj)
        {
            IsSelected = isSelected;
            Object = obj;
        }

        /// <summary>
        /// IsSelected
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Object
        /// </summary>
        public T Object { get; set; }
    }
}
