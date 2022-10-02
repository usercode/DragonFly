// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// HtmlField
/// </summary>
[FieldOptions(typeof(HtmlFieldOptions))]
public class HtmlField : TextBaseField
{
    public HtmlField()
    {

    }

    public HtmlField(string? text)
    {
        Value = text;
    }

}
