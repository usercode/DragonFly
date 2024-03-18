// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// QuoteBlock
/// </summary>
public class QuoteBlock : Block
{
    public override string CssIcon => "fa-solid fa-quote-left";

    public string? Text { get; set; }

    public string? Caption { get; set; }

    public string? Url { get; set; }
}
