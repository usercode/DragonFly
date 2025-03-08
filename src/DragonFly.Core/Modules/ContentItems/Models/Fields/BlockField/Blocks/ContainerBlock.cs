// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContainerBlock
/// </summary>
public class ContainerBlock : Block, IChildBlocks
{
    public ContainerBlock()
    {
    }

    public override string CssIcon => "fa-regular fa-square";

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; } = [];

    public IEnumerable<BlockContext> GetBlocks()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            int index = i;

            yield return new BlockContext(Blocks[i], x => Blocks[index] = x);
        }
    }
}
