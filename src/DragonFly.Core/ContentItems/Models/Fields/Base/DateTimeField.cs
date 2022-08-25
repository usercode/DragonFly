using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content;

[FieldOptions(typeof(DateTimeFieldOptions))]
public class DateTimeField : SingleValueField<DateTime?>
{
    public DateTimeField()
    {

    }

    public DateTimeField(DateTime? date)
    {
        Value = date;
    }
}
