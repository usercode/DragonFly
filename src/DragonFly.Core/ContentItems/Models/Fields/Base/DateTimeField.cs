// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly;

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
