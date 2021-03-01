using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// NumberPart
    /// </summary>
    [FieldOptions(typeof(FloatFieldOptions))]
    public class FloatField : SingleValueContentField<double?>
    {
        public FloatField()
        {

        }

        public FloatField(double? number)
        {
            Value = number;
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            FloatFieldOptions fieldOptions = (FloatFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            if (Value < fieldOptions.MinValue || Value > fieldOptions.MaxValue)
            {
                errors.AddRange(fieldName, fieldOptions.MinValue, fieldOptions.MaxValue);
            }

            return errors;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
