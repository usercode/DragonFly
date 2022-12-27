// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
