namespace DragonFly;

/// <summary>
/// BlockContext
/// </summary>
public class BlockContext
{
    public BlockContext(Block block, Action<Block> replacement)
    {
        Block = block;
        _replacement = replacement;
    }

    /// <summary>
    /// Block
    /// </summary>
    public Block Block { get; }

    private Action<Block> _replacement;

    /// <summary>
    /// ReplaceBlock
    /// </summary>
    public void ReplaceBlock(Block block)
    {
        _replacement?.Invoke(block);
    }
}

