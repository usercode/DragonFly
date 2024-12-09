// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// DateFieldOptions
/// </summary>
public class DateFieldOptions : SingleValueFieldOptions<DateOnly?>
{
    public override ContentField CreateContentField()
    {
        return new DateField();
    }
}
