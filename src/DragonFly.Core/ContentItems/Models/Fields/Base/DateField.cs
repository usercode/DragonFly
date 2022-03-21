using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content;

[FieldOptions(typeof(DateFieldOptions))]
public class DateField : SingleValueContentField<DateTime?>
{
    public DateField()
    {

    }

    public DateField(DateTime? date)
    {
        Value = date;
    }
}
