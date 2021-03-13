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

        public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
            DateFieldOptions fieldOptions = (DateFieldOptions)options;

            if (fieldOptions.IsRequired && HasValue == false)
            {
                context.AddRequireValidation(fieldName);
            }

        }

        public DateField(DateTime? date)
        {
            Value = date;
        }
    }
}
