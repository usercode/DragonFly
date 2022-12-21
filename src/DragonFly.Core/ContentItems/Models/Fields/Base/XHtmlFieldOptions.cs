// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class XHtmlFieldOptions : ContentFieldOptions
{
    public XHtmlFieldOptions()
    {
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// DefaultValue
    /// </summary>
    public string DefaultValue { get; set; }

    /// <summary>
    /// MinLength
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int MaxLength { get; set; }

    public override ContentField CreateContentField()
    {
        return new XHtmlField(DefaultValue);
    }
}
