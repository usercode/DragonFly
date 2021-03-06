﻿using DragonFly.Content;
using DragonFly.Contents.Assets;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Data.Models.Assets
{
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
                mongoAsset.Metaddata.Add(item.Key, item.Value.ToBsonDocument(item.Value.GetType()));
            }

            return mongoAsset;
        }

        public static Asset FromMongo(this MongoAsset mongoAsset)
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
                asset.SetMetadata((AssetMetadata)BsonSerializer.Deserialize(item.Value, AssetMetadataManager.Default.GetMetadataType(item.Key)));
            }

            return asset;
        }
    }
}
