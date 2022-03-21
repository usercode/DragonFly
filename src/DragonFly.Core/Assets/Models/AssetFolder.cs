using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content;

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
