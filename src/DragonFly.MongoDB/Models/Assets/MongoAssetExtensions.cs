using DragonFly.MongoDB.Proxies;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.MongoDB;

static class MongoAssetExtensions
{
    public static MongoAsset ToMongo(this Asset asset, bool update = false)
    {
        MongoAsset mongoAsset = new MongoAsset()
        {
            Id = asset.Id,
            CreatedAt = asset.CreatedAt,
            ModifiedAt = asset.ModifiedAt,
            PublishedAt = asset.PublishedAt,
            Version = asset.Version,
            Name = asset.Name,
            Alt = asset.Alt,
            Description = asset.Description,
            Slug = asset.Slug,
            Hash = asset.Hash,
            MimeType = asset.MimeType,
            Size = asset.Size,
            Folder = asset.Folder?.Id
        };

        foreach (var item in asset.Metaddata)
        {
            mongoAsset.Metaddata.Add(item.Key, item.Value.ToMongo());
        }

        return mongoAsset;
    }

    public static Asset ToModel(this MongoAsset mongoAsset)
    {
        Asset asset = new Asset()
        {
            Id = mongoAsset.Id,
            CreatedAt = mongoAsset.CreatedAt,
            ModifiedAt = mongoAsset.ModifiedAt,
            PublishedAt = mongoAsset.PublishedAt,
            Version = mongoAsset.Version,
            Name = mongoAsset.Name,
            Alt = mongoAsset.Alt,
            Description = mongoAsset.Description,
            Slug = mongoAsset.Slug,
            Hash = mongoAsset.Hash,
            MimeType = mongoAsset.MimeType,
            Size = mongoAsset.Size,
        };

        if (mongoAsset.Folder != null)
        {
            asset.Folder = ContentItemProxy.CreateAssetFolder(mongoAsset.Folder.Value);
        }

        foreach (var item in mongoAsset.Metaddata)
        {
            asset.SetMetadata(item.Value.ToModel(item.Key));
        }

        return asset;
    }

    public static BsonDocument ToMongo(this AssetMetadata metadata)
    {
        return metadata.ToBsonDocument(metadata.GetType());
    }

    public static AssetMetadata ToModel(this BsonDocument document, string name)
    {
        return (AssetMetadata)BsonSerializer.Deserialize(document, AssetMetadataManager.Default.GetMetadataType(name));
    }
}
