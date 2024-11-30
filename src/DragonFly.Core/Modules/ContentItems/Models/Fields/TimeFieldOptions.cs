// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// TimeFieldOptions
/// </summary>
public class TimeFieldOptions : SingleValueFieldOptions<TimeOnly?>
{
    public TimeFieldOptions()
    {
    }

    public override ContentField CreateContentField()
    {
        return new TimeField();
    }
}
