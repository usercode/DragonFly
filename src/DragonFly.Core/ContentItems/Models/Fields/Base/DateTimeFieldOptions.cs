﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// DateFieldOptions
/// </summary>
public class DateTimeFieldOptions : ContentFieldOptions
{
    public DateTimeFieldOptions()
    {
    }

    public override ContentField CreateContentField()
    {
        return new DateTimeField();
    }
}
