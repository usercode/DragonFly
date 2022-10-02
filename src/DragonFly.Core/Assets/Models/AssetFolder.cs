// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly;

/// <summary>
/// AssetFolder
/// </summary>
public class AssetFolder : ContentBase
{
    public AssetFolder()
    {
        Name = "Unknown";
    }

    public AssetFolder(Guid id)
        : this()
    {
        Id = id;
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
