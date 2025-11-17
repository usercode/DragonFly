// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

[Field]
[FieldOptions(typeof(DateTimeFieldOptions))]
public partial class DateTimeField : SingleValueField<DateTime?>
{
    public DateTimeField()
    {

    }

    public DateTimeField(DateTime? date)
    {
        Value = date;
    }
}
