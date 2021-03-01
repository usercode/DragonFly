using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DragonFly.AspNetCore.Services;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Data.Models;
using DragonFly.Data.Models.Assets;
using DragonFly.Data.Models.WebHooks;
using DragonFly.Models;
using DragonFly.MongoDB.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace DragonFly.Data
{
    /// <summary>
    /// MongoStorage
    /// </summary>
    public partial class MongoStorage : IDataStorage
    {
        public IMongoClient Client { get; }
        public IMongoDatabase OfmlDb { get; }

        public IMongoCollection<MongoContentSchema> ContentSchemas { get; }
        public IMongoCollection<MongoContentSchema> ContentSchemasPublished { get; }
        public IMongoCollection<MongoAssetFolder> AssetFolders { get; }
        public IMongoCollection<MongoAssetFolder> AssetFoldersPublished { get; }
        public IMongoCollection<MongoAsset> Assets { get; }
        public IMongoCollection<MongoAsset> AssetsPublished { get; }
        public IGridFSBucket AssetData { get; private set; }
        public IGridFSBucket AssetDataPublished { get; private set; }

        public IMongoCollection<MongoWebHook> WebHooks { get; }

        private IEnumerable<IAssetProcessing> AssetProcessings { get; }

        private IEnumerable<IContentInterceptor> Interceptors { get; }

        public IDateTimeService DateTimeService { get; }

        private static MongoStorage _default;

        public static MongoStorage Default
        {
            get => _default;
            set => _default = value;
        }

        public MongoStorage(
            IDateTimeService dateTimeService,
            IOptions<MongoDbOptions> options, 
            IEnumerable<IAssetProcessing> assetProcessings, 
            IEnumerable<IContentInterceptor> interceptors)
        {
            Default = this;

            Options = options.Value;

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(Options.Hostname);

            if (string.IsNullOrEmpty(Options.Username) == false)
            {
                settings.Credential = MongoCredential.CreateCredential("admin", Options.Username, Options.Password);
            }

            Client = new MongoClient(settings); // connect to localhost

            OfmlDb = Client.GetDatabase(Options.Database);

            ContentSchemas = OfmlDb.GetCollection<MongoContentSchema>("ContentSchemas");
            WebHooks = OfmlDb.GetCollection<MongoWebHook>("WebHooks");
            Assets = OfmlDb.GetCollection<MongoAsset>("Assets");
            AssetFolders = OfmlDb.GetCollection<MongoAssetFolder>("AssetFolders");

            AssetData = new GridFSBucket(OfmlDb, new GridFSBucketOptions() { BucketName = "Assets" });

            ContentItems = new Dictionary<string, IMongoCollection<MongoContentItem>>();

            DateTimeService = dateTimeService;
            AssetProcessings = assetProcessings;
            Interceptors = interceptors;
        }

        public MongoDbOptions Options { get; }

        private IDictionary<string, IMongoCollection<MongoContentItem>> ContentItems { get; }       

        public virtual void CreateIndex()
        {
            //Pages.Indexes.CreateOne(Builders<Basecl>.IndexKeys.Ascending(x => x.Title));
        }

        public async Task<ContentItem> GetContentItemAsync(string schema, Guid id)
        {
            ContentSchema contentSchema = await GetContentSchemaAsync(schema);
            var items = GetMongoCollection(schema);

            var result = items.AsQueryable().FirstOrDefault(x => x.Id == id);
            
            if(result == null)
            {
                throw new Exception("Not Found");
            }

            return result.ToModel(contentSchema);
        }
       
    }
}
