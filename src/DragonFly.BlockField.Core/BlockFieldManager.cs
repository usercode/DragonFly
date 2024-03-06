// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldManager
/// </summary>
public sealed class BlockFieldManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static BlockFieldManager Default { get; } = new BlockFieldManager();

    private IDictionary<string, BlockFactory> _blocksByName = new Dictionary<string, BlockFactory>();
    private IDictionary<Type, BlockFactory> _blocksByType = new Dictionary<Type, BlockFactory>();

    private BlockFactory[]? _currentBlockFactories = null;
    private bool _rebuildBlockList = false;

    /// <summary>
    /// Gets all available block factories.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BlockFactory> GetAllBlocks()
    {
        if (_currentBlockFactories == null || _rebuildBlockList)
        {
            _currentBlockFactories = _blocksByType.Values
                                        .Where(x => x.BlockType != typeof(UnknownBlock))
                                        .OrderBy(x => x.BlockName)                                        
                                        .ToArray();

            _rebuildBlockList = false;
        }

        return _currentBlockFactories;
    }

    private BlockFactory CreateFactory<TBlock>()
         where TBlock : Block, new()
    {
        Type blockType = typeof(TBlock);

        return new BlockFactory(blockType.Name, blockType, new TBlock().CssIcon, () => new TBlock());
    }

    /// <summary>
    /// Adds <typeparamref name="TBlock"/>.
    /// </summary>
    public void Add<TBlock>()
        where TBlock : Block, new()
    {
        BlockFactory factory = CreateFactory<TBlock>();

        _blocksByName[factory.BlockName] = factory;
        _blocksByType[factory.BlockType] = factory;

        _rebuildBlockList = true;
    }

    /// <summary>
    /// Removes <typeparamref name="TBlock"/>.
    /// </summary>
    /// <typeparam name="TBlock"></typeparam>
    public void Remove<TBlock>()
         where TBlock : Block, new()
    {
        BlockFactory factory = CreateFactory<TBlock>();

        _blocksByName.Remove(factory.BlockName);
        _blocksByType.Remove(factory.BlockType);

        _rebuildBlockList = true;
    }

    /// <summary>
    /// Removes all blocks.
    /// </summary>
    public void Clear()
    {
        _blocksByName.Clear();
        _blocksByType.Clear();

        _rebuildBlockList = true;
    }

    /// <summary>
    /// TryGetBlockTypeByName
    /// </summary>
    /// <param name="name"></param>
    /// <param name="blockType"></param>
    /// <returns></returns>
    public bool TryGetBlockTypeByName(string name, [NotNullWhen(true)] out Type? blockType)
    {
        if (_blocksByName.TryGetValue(name, out BlockFactory? factory))
        {
            blockType = factory.BlockType;
            return true;
        }

        blockType = null;
        return false;
    }

    /// <summary>
    /// TryGetBlockNameByType
    /// </summary>
    /// <param name="type"></param>
    /// <param name="blockName"></param>
    /// <returns></returns>
    public bool TryGetBlockNameByType(Type type, [NotNullWhen(true)] out string? blockName)
    {
        if (_blocksByType.TryGetValue(type, out BlockFactory? factory))
        {
            blockName = factory.BlockName;
            return true;
        }

        blockName = null;
        return false;
    }
}
