// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Core.Modules.Assets.Actions;

/// <summary>
/// AssetActionItem
/// </summary>
public class AssetActionItem
{
    public AssetActionItem(string name, Type type)
    {
        Name = name;
        Type = type;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public Type Type { get; set; }
}
