// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
