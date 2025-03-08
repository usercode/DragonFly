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
