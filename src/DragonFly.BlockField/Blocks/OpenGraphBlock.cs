// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// OpenGraphBlock
/// </summary>
public class OpenGraphBlock : Block
{
    public override string CssIcon => "fa-solid fa-globe";

    public string? Url { get; set; }
}
