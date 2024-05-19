// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using MongoDB.Driver;

namespace DragonFly.MongoDB.Storages;

public static class MongoClientExtensions
{
    public static IMongoCollection<MongoContentItem> GetContentCollection(this IMongoDatabase database, string type, bool published = false)
    {
        string name = $"ContentItems_{type}";

        if (published)
        {
            name += "_Published";
        }

        return database.GetCollection<MongoContentItem>(name);
    }

    public static IMongoCollection<MongoContentVersion> GetContentVersionCollection(this IMongoDatabase database, string type)
    {
        string name = $"ContentItems_{type}_Version";

        return database.GetCollection<MongoContentVersion>(name);
    }

    public static IMongoCollection<MongoAsset> GetAssetCollection(this IMongoDatabase database)
    {
        return database.GetCollection<MongoAsset>("Assets");
    }

    public static IMongoCollection<MongoAssetFolder> GetAssetFolderCollection(this IMongoDatabase database)
    {
        return database.GetCollection<MongoAssetFolder>("AssetFolders");
    }

    public static IMongoCollection<MongoContentSchema> GetSchemaCollection(this IMongoDatabase database)
    {
        return database.GetCollection<MongoContentSchema>("ContentSchemas");
    }

    public static async Task AddIndexAsync<T>(this IMongoCollection<T> collection, Expression<Func<T, object?>> fieldSelector)
    {
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<T>(Builders<T>.IndexKeys.Ascending(fieldSelector)));
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<T>(Builders<T>.IndexKeys.Descending(fieldSelector)));
    }
}
