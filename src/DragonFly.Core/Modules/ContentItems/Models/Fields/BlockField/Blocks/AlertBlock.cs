// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AlertBlock
/// </summary>
public class AlertBlock : Block, IChildBlocks
{
    public AlertBlock()
    {
    }

    public AlertBlock(ColorType colorType, params Block[] blocks)
        : this()
    {
        ColorType = colorType;
        Blocks = blocks;
    }

    public override string CssIcon => "fa-solid fa-circle-info";

    public IList<Block> Blocks { get; set; } = [];

    public ColorType ColorType { get; set; } = ColorType.Info;

    public IEnumerable<BlockContext> GetBlocks()
    {
        for (int i = 0; i < Blocks.Count; i++)
        {
            int index = i;

            yield return new BlockContext(Blocks[i], x => Blocks[index] = x);
        }
    }
}
