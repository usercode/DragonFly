using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Results;

namespace DragonFly.MongoDB;

public partial class MongoStorage : IContentVersionStorage
{
    public async Task<Result<IEnumerable<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        var collection = GetMongoCollectionVersioning(schema);

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
        IMongoCollection<MongoContentVersion> collection = GetMongoCollectionVersioning(schema);

        MongoContentVersion contentItem = await collection.AsQueryable().FirstOrDefaultAsync(x=> x.Id == id);

        if (contentItem == null)
        {
            return Result.Ok();
        }

        ContentSchema? contentSchema = await GetSchemaAsync(schema);

        return contentItem.Content.ToModel(contentSchema);
    }
}
