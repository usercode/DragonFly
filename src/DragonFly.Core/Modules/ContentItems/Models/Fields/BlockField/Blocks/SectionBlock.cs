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
        Blocks = new List<Block>();
    }

    public override string CssIcon => "fa-regular fa-square";

    /// <summary>
    /// Css
    /// </summary>
    public string? Css { get; set; }

    public IList<Block> Blocks { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
