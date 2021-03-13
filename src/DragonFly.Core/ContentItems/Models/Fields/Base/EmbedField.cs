using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    /// <summary>
    /// EmbedField
    /// </summary>
    [FieldOptions(typeof(EmbedFieldOptions))]
    public class EmbedField : ContentField
    {
        public EmbedField()
        {
        }

        /// <summary>
        /// ContentEmbedded
        /// </summary>
        public ContentEmbedded? ContentEmbedded { get; set; }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            EmbedFieldOptions fieldOptions = (EmbedFieldOptions)options;

            if (fieldOptions.IsRequired && ContentEmbedded == null)
            {
                context.AddRequireValidation(fieldName);
            }
        }

        public override string ToString()
        {
            if (ContentEmbedded == null)
            {
                return "no embed";
            }
            else
            {
                return $"{ContentEmbedded}";
            }
        }
    }
}
