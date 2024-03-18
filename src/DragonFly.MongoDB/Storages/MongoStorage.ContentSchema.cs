// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Index;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : ISchemaStorage
{
    public async Task CreateAsync(ContentSchema schema)
    {
        if (schema.Id == Guid.Empty)
        {
            schema.Id = Guid.NewGuid();
        }

        DateTime now = DateTimeService.Current();

        schema.CreatedAt = now;
        schema.ModifiedAt = now;

        MongoContentSchema mongo = schema.ToMongo();

        await ContentSchemas.InsertOneAsync(mongo);

        schema.Id = mongo.Id;

        await CreateIndicesAsync(schema);
    }

    public async Task UpdateAsync(ContentSchema schema)
    {
        schema.ModifiedAt = DateTimeService.Current();

        await ContentSchemas.FindOneAndReplaceAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, schema.Id), schema.ToMongo());

        await CreateIndicesAsync(schema);
    }

    public async Task DeleteAsync(ContentSchema schema)
    {
        await ContentSchemas.DeleteManyAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, schema.Id));
    }

    public async Task<QueryResult<ContentSchema>> QuerySchemasAsync()
    {
        IList<MongoContentSchema> r = await ContentSchemas.AsQueryable()
                                                            .OrderBy(x => x.Name)
                                                            .ToListAsync();

        return new QueryResult<ContentSchema>()
        {
            Items = r
                    .Select(x => x.ToModel())
                    .ToList()
        };
    }

    public async Task<ContentSchema?> GetSchemaAsync(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        MongoContentSchema? schema = await ContentSchemas.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);

        if (schema == null)
        {
            return null;
        }

        return schema.ToModel();
    }

    public async Task<ContentSchema?> GetSchemaAsync(Guid id)
    {
        MongoContentSchema? schema = await ContentSchemas.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        if (schema == null)
        {
            return null;
        }

        return schema.ToModel();
    }

    public async Task CreateIndicesAsync(ContentSchema schema)
    {
        //index for drafts
        await CreateIndicesInternalAsync(GetMongoCollection(schema.Name, false));

        //index for published
        await CreateIndicesInternalAsync(GetMongoCollection(schema.Name, true));

        async Task CreateIndicesInternalAsync(IMongoCollection<MongoContentItem> collection)
        {
            await collection.Indexes.DropAllAsync();

            foreach (var field in schema.Fields)
            {
                if (field.Value.Options?.IsSearchable == false)
                {
                    continue;
                }

                Type? fieldType = Api.Field().GetFieldType(field.Value.FieldType);

                if (fieldType == null)
                {
                    throw new Exception($"Content field not found for '{field.Value.FieldType}'.");
                }

                //add new indices
                if (Api.MongoIndex().TryGetByType(fieldType, out FieldIndex? fieldIndex))
                {
                    await fieldIndex.CreateIndexAsync(collection.Indexes, field.Key);
                }
                else
                {
                    Logger.LogWarning($"No index field found for '{fieldType.Name}'.");
                }
            }
        }
    }
}
