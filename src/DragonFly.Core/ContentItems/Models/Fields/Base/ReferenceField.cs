using DragonFly.Content.ContentParts;
using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Core.ContentItems.Models.Fields.Base;
using DragonFly.Data.Content.ContentParts;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content.Parts.Base
{
    /// <summary>
    /// ReferenceField
    /// </summary>
    [FieldOptions(typeof(ReferenceFieldOptions))]
    public class ReferenceField : ContentField
    {
        public const string IdField = "Id";
        public const string SchemaField = "Schema";

        public ReferenceField()
        {
        }

        /// <summary>
        /// ContentItem
        /// </summary>
        public ContentItem ContentItem { get; set; }

        public override ContentFieldOptions CreateOptions()
        {
            return new ReferenceFieldOptions();
        }

        public override string ToString()
        {
            if(ContentItem == null)
            {
                return "no reference";
            }

            return ContentItem.ToString();
        }
    }
}
