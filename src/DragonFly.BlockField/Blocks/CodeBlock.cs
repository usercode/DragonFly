// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// CodeBlock
/// </summary>
public class CodeBlock : Block
{
    public override string CssIcon => "fa-solid fa-code";

    /// <summary>
    /// CodeType
    /// </summary>
    public CodeType CodeType { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; }
}
