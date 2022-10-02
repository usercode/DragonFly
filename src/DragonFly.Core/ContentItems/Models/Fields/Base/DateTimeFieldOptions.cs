// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// DateFieldOptions
/// </summary>
public class DateTimeFieldOptions : ContentFieldOptions
{
    public DateTimeFieldOptions()
    {
    }

    public override IContentField CreateContentField()
    {
        return new DateTimeField();
    }
}
