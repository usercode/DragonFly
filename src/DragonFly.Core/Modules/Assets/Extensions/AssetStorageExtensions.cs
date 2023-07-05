// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class AssetStorageExtensions
{
    public static async Task<Asset> GetRequiredAssetAsync(this IAssetStorage storage, Guid id)
    {
        Asset? asset = await storage.GetAssetAsync(id);

        if (asset == null)
        {
            throw new Exception("Asset was not found.");
        }

        return asset;
    }
}
