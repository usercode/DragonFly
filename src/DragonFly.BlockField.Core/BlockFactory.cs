// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// BlockFactory
/// </summary>
public sealed class BlockFactory
{
    public BlockFactory(string blockName, Type blockField, string cssIcon, Func<Block> factoryBlock)
    {
        BlockName = blockName;
        BlockType = blockField;
        CssIcon = cssIcon;
        _factoryBlock = factoryBlock;
    }

    /// <summary>
    /// BlockName
    /// </summary>
    public string BlockName { get; }

    /// <summary>
    /// BlockType
    /// </summary>
    public Type BlockType { get; }

    /// <summary>
    /// CssIcon
    /// </summary>
    public string CssIcon { get; }

    private Func<Block> _factoryBlock;

    /// <summary>
    /// CreateBlock
    /// </summary>
    /// <returns></returns>
    public Block CreateBlock() => _factoryBlock();

    public string GetDisplayName()
    {
        const string block = "Block";

        if (BlockName.EndsWith(block))
        {
            return BlockName[0..^block.Length];
        }
        else
        {
            return BlockName;
        }
    }
}
