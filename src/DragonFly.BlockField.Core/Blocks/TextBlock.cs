// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// TextBlock
/// </summary>
public class TextBlock : Block
{
    public TextBlock()
    {
            
    }

    public TextBlock(string? text)
    {
        Text = text;
    }

    public override string CssIcon => "fa-solid fa-align-left";

    public string? Text { get; set; }
}
