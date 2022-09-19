using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

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

                _default.Add<ColumnBlock>();
                _default.Add<AssetBlock>();
                _default.Add<TextBlock>();
                _default.Add<HtmlBlock>();
                _default.Add<YouTubeBlock>();                
                _default.Add<CodeBlock>();
                _default.Add<OpenGraphBlock>();
                _default.Add<HeadingBlock>();
                _default.Add<QuoteBlock>();
                _default.Add<ReferenceBlock>();

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
