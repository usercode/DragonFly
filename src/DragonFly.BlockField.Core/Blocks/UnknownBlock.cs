// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// UnknownBlock
/// </summary>
public class UnknownBlock : Block
{
    public UnknownBlock()
    {

    }

    public UnknownBlock(string? content)
    {
        Content = content;
    }

    public string? Content { get; set; }
}
