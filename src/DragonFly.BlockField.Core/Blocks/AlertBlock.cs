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
    }

    public AlertBlock(ColorType colorType, params Block[] blocks)
        : this()
    {
        ColorType = colorType;
        Blocks = blocks;
    }

    public override string CssIcon => "fa-solid fa-circle-info";

    public IList<Block> Blocks { get; set; } = new List<Block>();

    public ColorType ColorType { get; set; } = ColorType.Info;

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
