// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.BlockField;

/// <summary>
/// UnknownBlock
/// </summary>
public class UnknownBlock : Block
{
    public UnknownBlock()
    {

    }

    public UnknownBlock(JsonElement node)
    {
        Node = node;
    }

    public JsonElement Node { get; set; }
}
