using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    [FieldOptions(typeof(DateFieldOptions))]
    public class DateField : SingleValueContentFieldNullable<DateTime>
    {
        public DateField()
        {

        }

        public DateField(DateTime? date)
        {
            Value = date;
        }
    }
}
