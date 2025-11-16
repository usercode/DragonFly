// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Index;
using DragonFly.MongoDB.Storages;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentSchemaMongoStorage
/// </summary>
public class ContentSchemaMongoStorage : MongoStorage, ISchemaStorage
{
    public ContentSchemaMongoStorage(MongoClient client, IDateTimeService dateTimeService, IDragonFlyApi api, ILogger<ContentSchemaMongoStorage> logger)
        : base(client)
    {
        DateTimeService = dateTimeService;
        Api = api;
        Logger = logger;

        ContentSchemas = Client.Database.GetSchemaCollection();
    }

    /// <summary>
    /// ContentSchemas
    /// </summary>
    public IMongoCollection<MongoContentSchema> ContentSchemas { get; }

    /// <summary>
    /// DateTimeService
    /// </summary>
    private IDateTimeService DateTimeService { get; }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Logger
    /// </summary>
    private ILogger<ContentSchemaMongoStorage> Logger { get; }

    public async Task<Result> CreateAsync(ContentSchema schema)
    {
        if (schema.Id == Guid.Empty)
        {
            schema.Id = Guid.NewGuid();
        }

        DateTime now = DateTimeService.Current();

        schema.CreatedAt = now;
        schema.ModifiedAt = now;

        MongoContentSchema mongo = schema.ToMongo();

        await ContentSchemas.InsertOneAsync(mongo).ConfigureAwait(false);

        schema.Id = mongo.Id;

        await CreateContentIndicesAsync(schema).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(ContentSchema schema)
    {
        schema.ModifiedAt = DateTimeService.Current();

        await ContentSchemas.FindOneAndReplaceAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, schema.Id), schema.ToMongo()).ConfigureAwait(false);

        await CreateContentIndicesAsync(schema).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(ContentSchema schema)
    {
        await ContentSchemas.DeleteManyAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, schema.Id)).ConfigureAwait(false);

        return Result.Ok();
    }

    public async Task<Result<QueryResult<ContentSchema>>> QuerySchemasAsync()
    {
        IList<MongoContentSchema> r = await ContentSchemas.AsQueryable()
                                                            .OrderBy(x => x.Name)
                                                            .ToListAsync()
                                                            .ConfigureAwait(false);

        return new QueryResult<ContentSchema>()
        {
            Items = r
                    .Select(x => x.ToModel())
                    .ToList()
        };
    }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        MongoContentSchema? schema = await ContentSchemas.AsQueryable().FirstOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);

        if (schema == null)
        {
            return null;
        }

        return schema.ToModel();
    }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(Guid id)
    {
        MongoContentSchema? schema = await ContentSchemas.AsQueryable().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (schema == null)
        {
            return null;
        }

        return schema.ToModel();
    }

    public async Task<Result> CreateContentIndicesAsync(ContentSchema schema)
    {
        //index for drafts
        await CreateIndicesInternalAsync(Client.Database.GetContentCollection(schema.Name, false)).ConfigureAwait(false);

        //index for published
        await CreateIndicesInternalAsync(Client.Database.GetContentCollection(schema.Name, true)).ConfigureAwait(false);

        async Task CreateIndicesInternalAsync(IMongoCollection<MongoContentItem> collection)
        {
            await collection.Indexes.DropAllAsync().ConfigureAwait(false);

            foreach (var field in schema.Fields)
            {
                if (field.Value.Options?.HasIndex == false)
                {
                    continue;
                }

                Type? fieldType = Api.Fields.GetFieldType(field.Value.FieldType);

                if (fieldType == null)
                {
                    throw new Exception($"Content field not found for '{field.Value.FieldType}'.");
                }

                //add new indices
                if (Api.MongoIndexes.TryGetByType(fieldType, out FieldIndex? fieldIndex))
                {
                    await fieldIndex.CreateIndexAsync(collection.Indexes, field.Key).ConfigureAwait(false);
                }
                else
                {
                    Logger.LogWarning($"No index field found for '{fieldType.Name}'.");
                }
            }
        }

        return Result.Ok();
    }
}
