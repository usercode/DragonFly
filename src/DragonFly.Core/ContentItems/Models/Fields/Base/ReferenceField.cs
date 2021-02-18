﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
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
