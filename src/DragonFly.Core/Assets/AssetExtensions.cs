// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class AssetDataUrlService
{
    public static string GetDataUrl(this Asset asset)
    {
        return $"/dragonfly/api/asset/{asset.Id}/download?v={asset.Hash}";
    }

    public static string GetFileSize(this Asset asset)
    {
        return $"{(double)asset.Size / 1024:###,###,##0.00} KB";
    }
}
