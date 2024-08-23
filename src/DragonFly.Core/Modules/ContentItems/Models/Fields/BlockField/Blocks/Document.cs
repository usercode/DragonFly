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
