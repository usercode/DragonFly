using DragonFly.Contents.Content;
using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Data.Content.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// ArrayField
    /// </summary>
    [FieldOptions(typeof(ArrayFieldOptions))]
    public class ArrayField : ContentField
    {
        public ArrayField()
        {
            Items = new List<ArrayFieldItem>();
        }

        /// <summary>
        /// Fields
        /// </summary>
        public IList<ArrayFieldItem> Items { get; set; }

        public override ContentFieldOptions CreateOptions()
        {
            return new ArrayFieldOptions();
        }
    }
}
