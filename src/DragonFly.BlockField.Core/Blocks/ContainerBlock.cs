// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// ContainerBlock
/// </summary>
public class ContainerBlock : Block, IChildBlocks
{
    public ContainerBlock()
    {
    }

    public override string CssIcon => "fa-regular fa-square";

    public IList<Block> Blocks { get; set; } = new List<Block>();

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
