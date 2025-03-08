// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// HeadingBlock
/// </summary>
public class HeadingBlock : Block
{
    public HeadingBlock()
    {
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
    /// HeadingType
    /// </summary>
    public HeadingType HeadingType { get; set; } = HeadingType.H1;
}
