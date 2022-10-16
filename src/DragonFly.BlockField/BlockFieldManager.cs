// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldManager
/// </summary>
public class BlockFieldManager
{
    private static BlockFieldManager? _default;

    /// <summary>
    /// Default
    /// </summary>
    public static BlockFieldManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new BlockFieldManager();

                _default.Add<UnknownBlock>();
            }

            return _default;
        }
    }

    private IDictionary<string, Type> _elementsByName;
    private IDictionary<Type, string> _elementsByType;

    private BlockFieldManager()
    {
        _elementsByName = new Dictionary<string, Type>();
        _elementsByType = new Dictionary<Type, string>();
    }

    public IEnumerable<Type> GetAllBlockTypes()
    {
        return _elementsByType.Keys.ToList();
    }

    public IEnumerable<Block> GetAllBlocks()
    {
        return _elementsByType.Keys
                                    .Where(x => x != typeof(UnknownBlock))
                                    .OrderBy(x => x.Name)
                                    .Select(x => (Block?)Activator.CreateInstance(x))
                                    .ToList();
    }

    /// <summary>
    /// RegisterElement
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public void Add<TBlock>()
        where TBlock : Block, new()
    {
        string typeName = new TBlock().Type;

        _elementsByName[typeName] = typeof(TBlock);
        _elementsByType[typeof(TBlock)] = typeName;
    }

    public bool TryGetBlockTypeByName(string name, [NotNullWhen(true)] out Type? type)
    {
        if (_elementsByName.TryGetValue(name, out type))
        {
            return true;
        }

        return false;
    }

    public bool TryGetBlockNameByType(Type type, [NotNullWhen(true)] out string? typeName)
    {
        if (_elementsByType.TryGetValue(type, out typeName))
        {
            return true;
        }

        return false;
    }
}
