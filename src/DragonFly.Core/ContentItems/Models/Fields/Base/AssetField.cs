using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    [FieldOptions(typeof(AssetFieldOptions))]
    public class AssetField : ContentField
    {
        public AssetField()
        {
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            AssetFieldOptions fieldOptions = (AssetFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && Asset == null)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }

        public Asset Asset { get; set; }
    }
}
