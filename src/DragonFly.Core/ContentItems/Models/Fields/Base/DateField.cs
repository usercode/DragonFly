using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    [FieldOptions(typeof(DateFieldOptions))]
    public class DateField : SingleValueContentField<DateTime?>
    {
        public DateField()
        {

        }

        public override IEnumerable<ValidationError> Validate(string fieldName, ContentFieldOptions options)
        {
            DateFieldOptions fieldOptions = (DateFieldOptions)options;
            IList<ValidationError> errors = new List<ValidationError>();

            if (fieldOptions.IsRequired && HasValue == false)
            {
                errors.AddRequire(fieldName);
            }

            return errors;
        }

        public DateField(DateTime? date)
        {
            Value = date;
        }
    }
}
