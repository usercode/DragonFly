// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// AlertBlock
/// </summary>
public class AlertBlock : Block, IChildBlocks
{
    public AlertBlock()
    {
        ColorType = ColorType.Info;
        Blocks = new List<Block>();
    }

    public AlertBlock(ColorType colorType, params Block[] blocks)
        : this()
    {
        ColorType = colorType;
        Blocks = blocks;
    }

    public override string CssIcon => "fa-solid fa-circle-info";

    public IList<Block> Blocks { get; set; }

    public ColorType ColorType { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
