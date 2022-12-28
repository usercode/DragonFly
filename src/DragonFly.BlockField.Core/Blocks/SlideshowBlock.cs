// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// SlideshowBlock
/// </summary>
public class SlideshowBlock : Block, IChildBlocks
{
    public SlideshowBlock()
    {
        Blocks = new List<Block>();
    }

    public override string CssIcon => "fa-regular fa-images";

    public IList<Block> Blocks { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
