// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

[Field]
[FieldOptions(typeof(TimeFieldOptions))]
public partial class TimeField : SingleValueField<TimeOnly?>
{
    public TimeField()
    {

    }

    public TimeField(TimeOnly? value)
    {
        Value = value;
    }
}
