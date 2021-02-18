using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ArrayFieldItem
    /// </summary>
    public class ArrayFieldItem : IContentItem
    {
        public ArrayFieldItem()
        {
            Fields = new ContentFields();
        }

        /// <summary>
        /// Fields
        /// </summary>
        public ContentFields Fields { get; set; }
    }
}
