// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class TextFieldOptions : ContentFieldOptions
{
    public TextFieldOptions()
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
        return new TextField(DefaultValue);
    }
}
