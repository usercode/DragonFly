// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB.Index;

public class GeolocationFieldIndex : FieldIndex
{
    public override Type FieldType => typeof(GeolocationField);

    public override async Task CreateIndexAsync(IMongoIndexManager<MongoContentItem> indexManager, string fieldName)
    {
        await indexManager.CreateOneAsync(
                        new CreateIndexModel<MongoContentItem>(Builders<MongoContentItem>.IndexKeys.Geo2DSphere(
                            CreateIndexPath(fieldName)),
                            new CreateIndexOptions()
                            {
                                Name = $"{CreateIndexPath(fieldName)}.geo"
                            }));
    }
}
