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

        public StringField(string text)
        {
            Value = text;
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            StringFieldOptions fieldOptions = (StringFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            if (HasValue)
            {
                if (Value.Length < fieldOptions.MinLength)
                {
                    errors.AddMinimum(fieldName, fieldOptions.MinLength);
                }
                else if (Value.Length > fieldOptions.MaxLength)
                {
                    errors.AddMaximum(fieldName, fieldOptions.MaxLength);
                }
            }

            return errors;
        }

        public override bool CanSorting => true;
    }
}
