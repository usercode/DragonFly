// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// HtmlFieldOptions
/// </summary>
public class HtmlFieldOptions : ContentFieldOptions
{
    public HtmlFieldOptions()
    {
    }

    public override IContentField CreateContentField()
    {
        return new HtmlField();
    }
}
