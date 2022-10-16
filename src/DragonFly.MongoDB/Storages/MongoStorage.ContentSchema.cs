// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
    }

    public async Task UpdateAsync(ContentSchema entity)
    {
        entity.ModifiedAt = DateTimeService.Current();

        await ContentSchemas.FindOneAndReplaceAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, entity.Id), entity.ToMongo());
    }

    public async Task DeleteAsync(ContentSchema entity)
    {
        await ContentSchemas.DeleteManyAsync(Builders<MongoContentSchema>.Filter.Eq(x => x.Id, entity.Id));
    }

    public async Task<QueryResult<ContentSchema>> QuerySchemasAsync()
    {
        return new QueryResult<ContentSchema>()
        {
            Items = ContentSchemas.AsQueryable()
                    .OrderBy(x => x.Name)
                    .ToList()
                    .Select(x => x.ToModel())
                    .ToList()
        };
    }

    public async Task<ContentSchema> GetSchemaAsync(string? name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        MongoContentSchema? schema = ContentSchemas.AsQueryable().FirstOrDefault(x => x.Name == name);

        if (schema == null)
        {
            return null;
        }

        return schema.ToModel();
    }

    public async Task<ContentSchema> GetSchemaAsync(Guid id)
    {
        MongoContentSchema? schema = ContentSchemas.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (schema == null)
        {
            throw new Exception($"Schema was not found: {id}");
        }

        return schema.ToModel();
    }
}
