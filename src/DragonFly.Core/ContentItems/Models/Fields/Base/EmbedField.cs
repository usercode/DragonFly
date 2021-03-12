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
        public ContentEmbedded ContentEmbedded { get; set; }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            EmbedFieldOptions fieldOptions = (EmbedFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && ContentEmbedded == null)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }

        public override string ToString()
        {
            if(ContentEmbedded == null)
            {
                return "no embed";
            }

            return ContentEmbedded.ToString();
        }
    }
}
