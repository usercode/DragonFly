// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB;

internal static class ProxyFactory
{
    public static ContentItem? CreateContent(string schema, Guid id)
    {
        var collection = MongoStorage.Default.GetMongoCollection(schema);

        if (collection.AsQueryable().Where(x => x.Id == id).Any())
        {
            return new ContentItemProxy(CreateSchema(schema), id);
        }
        else
        {
            return null;
        }
    }

    public static ContentSchema CreateSchema(string schema)
    {
        return new ContentSchemaProxy(schema);
    }

    public static Asset? CreateAsset(Guid assetId)
    {
        if (MongoStorage.Default.Assets.AsQueryable().Where(x => x.Id == assetId).Any())
        {
            return new AssetProxy(assetId);
        }
        else
        {
            return null;
        }
    }

    public static AssetFolder CreateAssetFolder(Guid assetFolderId)
    {
        return new AssetFolderProxy(assetFolderId);
    }
}
