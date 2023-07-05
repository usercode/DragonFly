// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

static class MongoFolderAssetExtensions
{
    public static MongoAssetFolder ToMongo(this AssetFolder asset)
    {
        MongoAssetFolder mongoAsset = new MongoAssetFolder()
        {
            Id = asset.Id,
            CreatedAt = asset.CreatedAt,
            ModifiedAt = asset.ModifiedAt,
            Version = asset.Version,
            Name = asset.Name,
            Parent = asset.Parent?.Id
        };

        return mongoAsset;
    }

    public static AssetFolder ToModel(this MongoAssetFolder mongoAsset)
    {
        AssetFolder asset = new AssetFolder()
        {
            Id = mongoAsset.Id,
            CreatedAt = mongoAsset.CreatedAt,
            ModifiedAt = mongoAsset.ModifiedAt,
            Version = mongoAsset.Version,
            Name = mongoAsset.Name,
        };

        if (mongoAsset.Parent != null)
        {
            asset.Parent = ProxyFactory.CreateAssetFolder(mongoAsset.Parent.Value);
        }

        return asset;
    }
}
