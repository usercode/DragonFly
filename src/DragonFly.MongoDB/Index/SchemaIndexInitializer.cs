﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.MongoDB.Storages;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// SchemaIndexInitializer
/// </summary>
class SchemaIndexInitializer : IPostInitialize
{
    public SchemaIndexInitializer(MongoClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public MongoClient Client { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        IMongoCollection<MongoContentSchema> schemas = Client.Database.GetSchemaCollection();

        //assets
        await schemas.AddIndexAsync($"{nameof(MongoContentSchema.Name)}");
        await schemas.AddIndexAsync($"{nameof(MongoContentSchema.CreatedAt)}");
        await schemas.AddIndexAsync($"{nameof(MongoContentSchema.ModifiedAt)}");
    }
}
