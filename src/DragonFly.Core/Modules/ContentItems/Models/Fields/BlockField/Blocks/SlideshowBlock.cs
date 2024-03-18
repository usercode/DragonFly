// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlideshowBlock
/// </summary>
public class SlideshowBlock : Block, IChildBlocks
{
    public SlideshowBlock()
    {
    }

    public override string CssIcon => "fa-regular fa-images";

    public IList<Block> Blocks { get; set; } = new List<Block>();

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
