// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;

namespace DragonFly.Core.Modules.Assets.Actions;

/// <summary>
/// AssetActionManager
/// </summary>
public class AssetActionManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static AssetActionManager Default { get; } = new AssetActionManager();

    private Dictionary<string, List<AssetActionItem>> _cache = new Dictionary<string, List<AssetActionItem>>();
    private Dictionary<string, AssetActionItem> _cacheByName = new Dictionary<string, AssetActionItem>();

    /// <summary>
    /// Add
    /// </summary>
    public void Add<T>(string name, string[] mimeTypes)
        where T : IAssetAction
    {
        AssetActionItem item = new AssetActionItem(name, typeof(T));

        _cacheByName[item.Name] = item;

        foreach (string mimeType in mimeTypes)
        {
            if (_cache.TryGetValue(mimeType, out List<AssetActionItem>? list) == false)
            {
                list = new List<AssetActionItem>();

                _cache[mimeType] = list;
            }

            list.Add(item);
        }
    }

    public AssetActionItem? GetByName(string name)
    {
        if (_cacheByName.TryGetValue(name, out AssetActionItem? type))
        {
            return type;
        }

        return null;
    }

    /// <summary>
    /// GetByMimeType
    /// </summary>
    public AssetActionItem[] GetByMimeType(string mimeType)
    {
        if (_cache.TryGetValue(mimeType, out List<AssetActionItem>? list) == false)
        {
            return [];
        }

        return list.ToArray();
    }
}
