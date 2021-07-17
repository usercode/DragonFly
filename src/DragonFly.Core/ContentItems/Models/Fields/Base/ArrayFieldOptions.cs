using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ArrayFieldOptions
    /// </summary>
    public class ArrayFieldOptions : ContentFieldOptions, ISchemaElement
    {
        public ArrayFieldOptions()
        {
            Fields = new ContentSchemaFields();
        }

        /// <summary>
        /// Fields
        /// </summary>
        public ContentSchemaFields Fields { get; set; }

        /// <summary>
        /// MinItems
        /// </summary>
        public int? MinItems { get; set; }

        /// <summary>
        /// MaxItems
        /// </summary>
        public int? MaxItems { get; set; }

        public override ContentField CreateContentField()
        {
            return new ArrayField();
        }
    }
}
