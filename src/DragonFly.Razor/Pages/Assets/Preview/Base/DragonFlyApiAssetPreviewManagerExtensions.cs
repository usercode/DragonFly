// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Razor.Assets;

namespace DragonFly;

/// <summary>
/// DragonFlyApiAssetPreviewManagerExtensions
/// </summary>
public static class DragonFlyApiAssetPreviewManagerExtensions
{
    public static AssetPreviewManager AssetPreview(this IDragonFlyApi api)
    {
        return AssetPreviewManager.Default;
    }
}
