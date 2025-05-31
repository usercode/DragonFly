// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.MongoDB.Index;
using DragonFly.MongoDB.Storages;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentIndexInitializer
/// </summary>
class ContentIndexInitializer : IPostInitialize
{
    public ContentIndexInitializer(MongoClient client, ILogger<AssetIndexInitializer> logger)
    {
        Client = client;
        Logger = logger;
    }

    /// <summary>
    /// Client
    /// </summary>
    public MongoClient Client { get; }

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<AssetIndexInitializer> Logger { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        IMongoCollection<MongoContentSchema> schemaCollection = Client.Database.GetSchemaCollection();

        //content
        IList<MongoContentSchema> schemas = await schemaCollection.AsQueryable().ToListAsync();

        foreach (ContentSchema schema in schemas.Select(x=> x.ToModel()))
        {
            var drafts = Client.Database.GetContentCollection(schema.Name, false);
            var published = Client.Database.GetContentCollection(schema.Name, true);

            //index for drafts
            await CreateIndicesInternalAsync(drafts);

            //index for published
            await CreateIndicesInternalAsync(published);

            async Task CreateIndicesInternalAsync(IMongoCollection<MongoContentItem> collection)
            {
                await collection.AddIndexAsync($"{nameof(MongoContentItem.CreatedAt)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.ModifiedAt)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.PublishedAt)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.Version)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.SchemaVersion)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.ReferencedTo)}.{nameof(MongoReferencedContent.Id2)}");
                await collection.AddIndexAsync($"{nameof(MongoContentItem.ReferencedTo)}.{nameof(MongoReferencedContent.Schema)}");

                foreach (var field in schema.Fields)
                {
                    if (field.Value.Options?.HasIndex == false)
                    {
                        continue;
                    }

                    Type? fieldType = FieldManager.Default.GetFieldType(field.Value.FieldType);

                    if (fieldType == null)
                    {
                        throw new Exception($"Content field not found for '{field.Value.FieldType}'.");
                    }

                    //add new indices
                    if (MongoIndexManager.Default.TryGetByType(fieldType, out FieldIndex? fieldIndex))
                    {
                        await fieldIndex.CreateIndexAsync(collection.Indexes, field.Key);
                    }
                    else
                    {
                        Logger.LogWarning("No index field found for \"{fieldType}\".", fieldType.Name);
                    }
                }
            }
        }
    }
}
