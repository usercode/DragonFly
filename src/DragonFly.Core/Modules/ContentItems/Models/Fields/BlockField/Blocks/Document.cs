// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Document
/// </summary>
public class Document : IChildBlocks
{
    public Document()
    {
    }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; } = new List<Block>();

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
