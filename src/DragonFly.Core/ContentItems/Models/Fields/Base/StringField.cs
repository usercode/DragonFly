using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// TextField
    /// </summary>
    [FieldOptions(typeof(StringFieldOptions))]
    public class StringField : TextBaseField
    {
        public StringField()
        {
        }

        public StringField(string? text)
        {
            Value = text;
        }

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            StringFieldOptions fieldOptions = (StringFieldOptions)options;

            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }

            if (HasValue)
            {
                if (Value.Length < fieldOptions.MinLength)
                {
                    context.AddMinimumValidation(fieldName, fieldOptions.MinLength);
                }
                else if (Value.Length > fieldOptions.MaxLength)
                {
                    context.AddMaximumValidation(fieldName, fieldOptions.MaxLength);
                }
            }
        }

        public override bool CanSorting => true;
    }
}
