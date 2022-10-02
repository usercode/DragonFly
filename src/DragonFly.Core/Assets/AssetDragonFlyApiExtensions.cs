// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// AssetDragonFlyApiExtensions
/// </summary>
public static class AssetDragonFlyApiExtensions
{
    public static AssetMetadataManager AssetMetadata(this IDragonFlyApi dragonFlyApi)
    {
        return AssetMetadataManager.Default;
    }

    public static AssetMetadataManager AddDefaults(this AssetMetadataManager manager)
    {
        manager.Add<ImageMetadata>();
        manager.Add<PdfMetadata>();

        return manager;
    }
}
