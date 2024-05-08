// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoClient
/// </summary>
public class MongoClient
{
    /// <summary>
    /// Client
    /// </summary>
    public IMongoClient Client { get; }

    /// <summary>
    /// Database
    /// </summary>
    public IMongoDatabase Database { get; }

    /// <summary>
    /// Options
    /// </summary>
    public MongoDbOptions Options { get; }

    public MongoClient(IOptions<MongoDbOptions> options)
    {
        Options = options.Value;

        MongoClientSettings settings = new MongoClientSettings();
        settings.Server = new MongoServerAddress(Options.Hostname, Options.Port);

        if (string.IsNullOrEmpty(Options.Username) == false)
        {
            settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
        }

        Client = new global::MongoDB.Driver.MongoClient(settings);

        Database = Client.GetDatabase(Options.Database);
    }

    public async Task DeleteDatabaseAsync()
    {
        await Client.DropDatabaseAsync(Options.Database);
    }
}
