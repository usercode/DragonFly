// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

internal static class ProxyFactory
{
    public static ContentItem CreateContent(string schema, Guid id)
    {
        return new ContentItemProxy(CreateSchema(schema), id);
    }

    public static ContentSchema CreateSchema(string schema)
    {
        return new ContentSchemaProxy(schema);
    }

    public static Asset CreateAsset(Guid assetId)
    {
        return new AssetProxy(assetId);
    }

    public static AssetFolder CreateAssetFolder(Guid assetFolderId)
    {
        return new AssetFolderProxy(assetFolderId);
    }
}
