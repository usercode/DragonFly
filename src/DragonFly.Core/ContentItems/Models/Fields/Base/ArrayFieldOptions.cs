using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ArrayFieldOptions
    /// </summary>
    public class ArrayFieldOptions : ContentFieldOptions, IContentSchema
    {
        public ArrayFieldOptions()
        {
            Fields = new ContentSchemaFields();
        }

        /// <summary>
        /// Fields
        /// </summary>
        public ContentSchemaFields Fields { get; set; }

        public override ContentField CreateContentField()
        {
            return new ArrayField();
        }
    }
}
