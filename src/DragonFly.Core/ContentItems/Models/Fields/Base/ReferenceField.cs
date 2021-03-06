﻿using DragonFly.Core.ContentItems.Models.Validations;
using System;
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
        public ContentItem? ContentItem { get; set; }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            ReferenceFieldOptions fieldOptions = (ReferenceFieldOptions)options;

            if (fieldOptions.IsRequired && ContentItem == null)
            {
                context.AddRequireValidation(fieldName);
            }
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
