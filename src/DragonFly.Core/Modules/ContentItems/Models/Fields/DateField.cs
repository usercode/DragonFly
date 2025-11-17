// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

[Field]
[FieldOptions(typeof(DateFieldOptions))]
public partial class DateField : SingleValueField<DateOnly?>
{
    public DateField()
    {

    }

    public DateField(DateOnly? date)
    {
        Value = date;
    }
}
