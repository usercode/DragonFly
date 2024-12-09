// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// HtmlFieldOptions
/// </summary>
public class HtmlFieldOptions : FieldOptions
{
    public override ContentField CreateContentField()
    {
        return new HtmlField();
    }
}
