// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly;

/// <summary>
/// HtmlField
/// </summary>
[Field]
[FieldOptions(typeof(HtmlFieldOptions))]
public partial class HtmlField : TextBaseField
{
    public HtmlField()
    {

    }

    public HtmlField(string? text)
    {
        Value = text;
    }

}
