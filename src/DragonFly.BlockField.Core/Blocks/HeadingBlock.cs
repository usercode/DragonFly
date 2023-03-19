// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// HeadingBlock
/// </summary>
public class HeadingBlock : Block
{
    public HeadingBlock()
    {
        HeadingType = HeadingType.H1;
    }

    public HeadingBlock(HeadingType type, string text)
        : this()
    {
        HeadingType = type;
        Text = text;
    }

    public override string CssIcon => "fa-solid fa-heading";

    /// <summary>
    /// Text
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// TextAlignment
    /// </summary>
    public TextAlignment? TextAlignment { get; set; }

    /// <summary>
    /// HeadingType
    /// </summary>
    public HeadingType HeadingType { get; set; }
}
