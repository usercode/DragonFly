using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerField
    /// </summary>
    [FieldOptions(typeof(IntegerFieldOptions))]
    public class IntegerField : SingleValueContentField<long?>
    {
        public IntegerField()
        {

        }

        public override bool CanSorting => true;

        public IntegerField(long? number)
        {
            Value = number;
        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            IntegerFieldOptions fieldOptions = (IntegerFieldOptions)options;
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
