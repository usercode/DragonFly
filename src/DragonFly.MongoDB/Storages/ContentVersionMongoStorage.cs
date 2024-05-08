using DragonFly.MongoDB.Storages;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartResults;

namespace DragonFly.MongoDB;

public class ContentVersionMongoStorage : MongoStorage, IContentVersionStorage
{
    public ContentVersionMongoStorage(
                        MongoClient client,
                        ISchemaStorage schemaStorage)
                        : base(client)
    {
        SchemaStorage = schemaStorage;
    }

    /// <summary>
    /// SchemaStorage
    /// </summary>
    private ISchemaStorage SchemaStorage { get; }

    public async Task<Result<IEnumerable<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        var collection = Client.Database.GetContentVersionCollection(schema);

        var result = await collection.AsQueryable()
                                            .Where(x => x.Content.Id == id)
                                            .Select(x => new ContentVersionEntry() {
                                                Id = x.Id,
                                                Modified = x.Content.ModifiedAt,
                                                PublishedAt = x.Content.PublishedAt,
                                                Version = x.Content.Version })
                                            .OrderByDescending(x => x.Modified)
                                            .ToListAsync();

        return result;
    }

    public async Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id)
    {        
        IMongoCollection<MongoContentVersion> collection = Client.Database.GetContentVersionCollection(schema);

        MongoContentVersion contentItem = await collection.AsQueryable().FirstOrDefaultAsync(x=> x.Id == id);

        if (contentItem == null)
        {
            return Result.Ok<ContentItem?>();
        }

        ContentSchema? contentSchema = await SchemaStorage.GetSchemaAsync(schema);

        return contentItem.Content.ToModel(contentSchema);
    }
}
