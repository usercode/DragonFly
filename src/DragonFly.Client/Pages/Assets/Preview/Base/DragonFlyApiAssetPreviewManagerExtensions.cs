// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

/// <summary>
/// DragonFlyApiAssetPreviewManagerExtensions
/// </summary>
public static class DragonFlyApiAssetPreviewManagerExtensions
{
    extension(IDragonFlyApi api)
    {
        public AssetPreviewManager AssetPreviews => AssetPreviewManager.Default;
    }
}
