using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Fields;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Data.Content.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
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
