// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// CodeBlock
/// </summary>
public class CodeBlock : Block
{
    public CodeBlock()
    {
        CodeType = CodeType.Plain;
    }

    public CodeBlock(CodeType codeType, string content)
        : this()
    {
        CodeType = codeType;
        Content = content;
    }

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
