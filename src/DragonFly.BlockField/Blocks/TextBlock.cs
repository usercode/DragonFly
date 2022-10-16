// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// TextBlock
/// </summary>
public class TextBlock : Block
{
    public override string CssIcon => "fa-solid fa-align-left";

    public string? Text { get; set; }
}
