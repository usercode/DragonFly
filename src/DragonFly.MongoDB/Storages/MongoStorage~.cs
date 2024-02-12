// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Options;
using DragonFly.AspNetCore;
using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStorage
/// </summary>
public partial class MongoStorage
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
    public IDateTimeService DateTimeService { get; }
    public ISlugService SlugService { get; }
    private IDragonFlyApi Api { get; }
    public ILogger<MongoStorage> Logger { get; }
    public IBackgroundTaskManager BackgroundTaskService { get; }

    private static MongoStorage? _default;

    /// <summary>
    /// Default
    /// </summary>
    public static MongoStorage Default
    {
        get => _default!;
        private set => _default = value;
    }

    public MongoStorage(
        IDateTimeService dateTimeService,
        IOptions<DragonFlyOptions> dragonFlyOptions,
        IOptions<MongoDbOptions> options, 
        IDragonFlyApi api,
        IBackgroundTaskManager backgroundTaskService,
        ISlugService slugService,
        ILogger<MongoStorage> logger)
    {
        Default = this;

        Logger = logger;
        Api = api;
        BackgroundTaskService = backgroundTaskService;
        SlugService = slugService;
        Options = options.Value;

        DragonFlyOptions = dragonFlyOptions.Value;

        CreateMissingFieldSerializers(api);

        MongoClientSettings settings = new MongoClientSettings();
        settings.Server = new MongoServerAddress(Options.Hostname, Options.Port);

        if (string.IsNullOrEmpty(Options.Username) == false)
        {
            settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
        }

        Client = new MongoClient(settings);

        Database = Client.GetDatabase(Options.Database);
        
        ContentStructures = Database.GetCollection<MongoContentStructure>("ContentStructures");
        ContentSchemas = Database.GetCollection<MongoContentSchema>("ContentSchemas");
        ContentNodes = Database.GetCollection<MongoContentNode>("ContentNodes");
        WebHooks = Database.GetCollection<MongoWebHook>("WebHooks");
        Assets = Database.GetCollection<MongoAsset>("Assets");
        AssetFolders = Database.GetCollection<MongoAssetFolder>("AssetFolders");
        Events = Database.GetCollection<MongoEvent>("Events");

        AssetData = new GridFSBucket(Database, new GridFSBucketOptions() { BucketName = "Assets" });

        DateTimeService = dateTimeService;
    }

    public void CreateMissingFieldSerializers(IDragonFlyApi api)
    {
        foreach (Type contentFieldType in api.ContentField().GetAllFieldTypes())
        {
            if (api.MongoField().TryGetByFieldType(contentFieldType, out IMongoFieldSerializer? fieldSerializer))
            {
                continue;
            }

            //build SingleValueSerializer?
            if (contentFieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
            {
                //create SingleValueFieldSerializer
                fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(SingleValueMongoFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create single value field serializer for '{contentFieldType.Name}'.");
                }

                api.MongoField().Add(fieldSerializer);
            }
            else //build DefaultFieldSerializer
            {
                fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(DefaultMongoFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create default field serializer for '{contentFieldType.Name}'.");
                }

                api.MongoField().Add(fieldSerializer);
            }
        }
    }    

    public async Task DeleteDatabaseAsync()
    {
        await Client.DropDatabaseAsync(Options.Database);
    }

    public DragonFlyOptions DragonFlyOptions { get; }

    public MongoDbOptions Options { get; }

    private IDictionary<string, IMongoCollection<MongoContentItem>> ContentItems { get; } = new Dictionary<string, IMongoCollection<MongoContentItem>>();

    private IDictionary<string, IMongoCollection<MongoContentItemVersion>> ContentItemsVersioning { get; } = new Dictionary<string, IMongoCollection<MongoContentItemVersion>>();
}
