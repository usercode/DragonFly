// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetFolder
/// </summary>
public class AssetFolder : ContentBase<AssetFolder>
{
    public AssetFolder()
    {
    }

    public AssetFolder(Guid id)
        : this()
    {
        _id = id;
    }

    /// <summary>
    /// Name
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public virtual AssetFolder? Parent { get; set; }
}
