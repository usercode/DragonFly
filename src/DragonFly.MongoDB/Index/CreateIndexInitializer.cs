﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// CreateIndexInitializer
/// </summary>
class CreateIndexInitializer : IPostInitialize
{
    public CreateIndexInitializer(MongoStorage mongoStorage, IDragonFlyApi api, ILogger<CreateIndexInitializer> logger)
    {
        MongoStorage = mongoStorage;
        Api = api;
        Logger = logger;
    }

    /// <summary>
    /// MongoStorage
    /// </summary>
    public MongoStorage MongoStorage { get; }

    /// <summary>
    /// Api
    /// </summary>
    public IDragonFlyApi Api { get; }

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<CreateIndexInitializer> Logger { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        //schema
        await MongoStorage.ContentSchemas.Indexes.CreateOneAsync(new CreateIndexModel<MongoContentSchema>(Builders<MongoContentSchema>.IndexKeys.Ascending(x => x.Name)));

        //assets
        await MongoStorage.Assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Name)));
        await MongoStorage.Assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Slug)));
        await MongoStorage.Assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Alt)));
        await MongoStorage.Assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.MimeType)));
        await MongoStorage.Assets.Indexes.CreateOneAsync(new CreateIndexModel<MongoAsset>(Builders<MongoAsset>.IndexKeys.Ascending(x => x.Size)));

        //events
        await MongoStorage.Events.Indexes.CreateOneAsync(new CreateIndexModel<MongoEvent>(Builders<MongoEvent>.IndexKeys.Ascending(x => x.Date)));
        await MongoStorage.Events.Indexes.CreateOneAsync(new CreateIndexModel<MongoEvent>(Builders<MongoEvent>.IndexKeys.Ascending(x => x.Name)));

        //content
        IList<MongoContentSchema> schemas = await MongoStorage.ContentSchemas.AsQueryable().ToListAsync();

        foreach (ContentSchema schema in schemas.Select(x=> x.ToModel()))
        {
            await MongoStorage.CreateIndicesAsync(schema);
        }
    }
}
