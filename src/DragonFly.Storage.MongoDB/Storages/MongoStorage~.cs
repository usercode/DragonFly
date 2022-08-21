using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.Services;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Data.Models.Assets;
using DragonFly.Data.Models.WebHooks;
using DragonFly.Models;
using DragonFly.MongoDB.Options;
using DragonFly.Storage;
using DragonFly.Storage.MongoDB.Models.ContentStructures;
using DragonFly.Storage.MongoDB.Models.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace DragonFly.Data;

/// <summary>
/// MongoStorage
/// </summary>
public partial class MongoStorage : IDataStorage
{
    public IMongoClient Client { get; }
    public IMongoDatabase Database { get; }

    public IMongoCollection<MongoContentSchema> ContentSchemas { get; }
    public IMongoCollection<MongoContentStructure> ContentStructures { get; }
    public IMongoCollection<MongoContentNode> ContentNodes { get; }
    public IMongoCollection<MongoAssetFolder> AssetFolders { get; }
    public IMongoCollection<MongoAsset> Assets { get; }
    public IGridFSBucket AssetData { get; private set; }
    public IMongoCollection<MongoWebHook> WebHooks { get; }
    public IMongoCollection<MongoEvent> Events { get; }

    private IEnumerable<IAssetProcessing> AssetProcessings { get; }

    private IEnumerable<IContentInterceptor> Interceptors { get; }

    public IDateTimeService DateTimeService { get; }

    private IDragonFlyApi Api { get; }

    public ILogger<MongoStorage> Logger { get; }

    private static MongoStorage? _default;

    public static MongoStorage Default
    {
        get => _default!;
        set => _default = value;
    }

    public MongoStorage(
        IDateTimeService dateTimeService,
        IOptions<MongoDbOptions> options, 
        IDragonFlyApi api,
        IEnumerable<IAssetProcessing> assetProcessings, 
        IEnumerable<IContentInterceptor> interceptors,
        ILogger<MongoStorage> logger)
    {
        Default = this;

        Logger = logger;
        Api = api;
        Options = options.Value;

        MongoClientSettings settings = new MongoClientSettings();
        settings.Server = new MongoServerAddress(Options.Hostname, Options.Port);

        if (string.IsNullOrEmpty(Options.Username) == false)
        {
            settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
        }

        Client = new MongoClient(settings); // connect to localhost

        Database = Client.GetDatabase(Options.Database);
        
        ContentStructures = Database.GetCollection<MongoContentStructure>("ContentStructures");
        ContentSchemas = Database.GetCollection<MongoContentSchema>("ContentSchemas");
        ContentNodes = Database.GetCollection<MongoContentNode>("ContentNodes");
        WebHooks = Database.GetCollection<MongoWebHook>("WebHooks");
        Assets = Database.GetCollection<MongoAsset>("Assets");
        AssetFolders = Database.GetCollection<MongoAssetFolder>("AssetFolders");
        Events = Database.GetCollection<MongoEvent>("Events");

        AssetData = new GridFSBucket(Database, new GridFSBucketOptions() { BucketName = "Assets" });

        ContentItems = new Dictionary<string, IMongoCollection<MongoContentItem>>();

        DateTimeService = dateTimeService;
        AssetProcessings = assetProcessings;
        Interceptors = interceptors;
    }

    public async Task DeleteDatabaseAsync()
    {
        await Client.DropDatabaseAsync(Options.Database);
    }

    public MongoDbOptions Options { get; }

    private IDictionary<string, IMongoCollection<MongoContentItem>> ContentItems { get; }
}
