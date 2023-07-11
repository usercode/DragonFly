// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB.Index;

/// <summary>
/// DefaultFieldIndex
/// </summary>
/// <typeparam name="T"></typeparam>
public class DefaultFieldIndex<T> : FieldIndex
    where T : ContentField
{
    public override Type FieldType => typeof(T);

    public override async Task CreateIndexAsync(IMongoIndexManager<MongoContentItem> indexManager, string fieldName)
    {
        //create new index
        await indexManager.CreateOneAsync(
                       new CreateIndexModel<MongoContentItem>(Builders<MongoContentItem>.IndexKeys.Ascending(
                           CreateIndexPath(fieldName)),
                           new CreateIndexOptions()
                           {
                               Name = fieldName,
                               Unique = Unique,
                               //Collation = new Collation(locale: "en", strength: CollationStrength.Primary)
                           }));
    }
}
