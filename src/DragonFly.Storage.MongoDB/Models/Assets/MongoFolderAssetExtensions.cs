using DragonFly.Contents.Assets;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Data.Models.Assets
{
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
                asset.Parent = ContentItemProxy.CreateAssetFolder(mongoAsset.Parent.Value);
            }

            return asset;
        }
    }
}
