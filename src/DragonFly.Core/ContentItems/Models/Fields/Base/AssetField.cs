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

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            AssetFieldOptions fieldOptions = (AssetFieldOptions)options;

            if (fieldOptions.IsRequired && Asset == null)
            {
                context.AddRequireValidation(fieldName);
            }
        }

        public Asset? Asset { get; set; }
    }
}
