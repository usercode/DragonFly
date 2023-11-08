// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// HtmlBlock
/// </summary>
public class HtmlBlock : Block
{
    public HtmlBlock()
    {
    }

    public HtmlBlock(string? html)
        : this()
    {
        HtmlText = html;
    }

    public override string CssIcon => "fa-regular fa-file-code";

    public string? HtmlText { get; set; }
}
