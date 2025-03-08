// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SectionBlock
/// </summary>
public class SectionBlock : Block, IChildBlocks
{
    public SectionBlock()
    {
    }

    public override string CssIcon => "fa-regular fa-square";

    /// <summary>
    /// Css
    /// </summary>
    public string? Css { get; set; }

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
