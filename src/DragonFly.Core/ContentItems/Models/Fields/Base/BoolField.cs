using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// BoolField
    /// </summary>
    [FieldOptions(typeof(BoolFieldOptions))]
    public class BoolField : SingleValueContentField<bool?>
    {
        public BoolField()
        {

        }

        public override bool CanSorting => true;

        public BoolField(bool? value)
        {
            Value = value;
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            BoolFieldOptions fieldOptions = (BoolFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
