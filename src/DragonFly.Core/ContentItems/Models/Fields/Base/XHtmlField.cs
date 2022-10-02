// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(XHtmlFieldOptions))]
public class XHtmlField : TextBaseField
{
    public XHtmlField()
    {

    }

    public XHtmlField(string? text)
    {
        Value = text;
    }
}
