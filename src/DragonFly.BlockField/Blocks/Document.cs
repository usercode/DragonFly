// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// Document
/// </summary>
public class Document : IChildBlocks
{
    public Document()
    {
        Blocks = new List<Block>();
    }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; }

    public IEnumerable<Block> GetBlocks()
    {
        return Blocks;
    }
}
