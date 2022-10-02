// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB;
using DragonFLy.ApiKeys.AspNetCore.Storage.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonFly.AspNetCore.Identity.MongoDB;

/// <summary>
/// MongIdentityStore
/// </summary>
class MongoIdentityStore
{
    public MongoIdentityStore(IOptions<MongoDbOptions> options)
    {
        Options = options.Value;

        MongoClientSettings settings = new MongoClientSettings();
        settings.Server = new MongoServerAddress(Options.Hostname, Options.Port);

        if (string.IsNullOrEmpty(Options.Username) == false)
        {
            settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
        }

        Client = new MongoClient(settings);

        Database = Client.GetDatabase(Options.Database);

        ApiKeys = Database.GetCollection<MongoApiKey>("ApiKeys");

        ApiKeys.Indexes.CreateOne(new CreateIndexModel<MongoApiKey>(Builders<MongoApiKey>.IndexKeys.Ascending(x => x.Name)));
        ApiKeys.Indexes.CreateOne(new CreateIndexModel<MongoApiKey>(Builders<MongoApiKey>.IndexKeys.Ascending(x => x.Value)));
        ApiKeys.Indexes.CreateOne(new CreateIndexModel<MongoApiKey>(Builders<MongoApiKey>.IndexKeys.Ascending(x => x.Permissions)));

    }

    public MongoDbOptions Options { get; }

    public MongoClient Client { get; }

    public IMongoDatabase Database { get; }

    public IMongoCollection<MongoApiKey> ApiKeys { get; }
}
