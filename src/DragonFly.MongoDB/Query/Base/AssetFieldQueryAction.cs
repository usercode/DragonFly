// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// AssetFieldQueryAction
/// </summary>
public class AssetFieldQueryAction : FieldQueryAction<AssetQuery>
{
    public override void Apply(AssetQuery query, FieldQueryActionContext context)
    {
        if (query.AssetId != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullFieldName(query.FieldName), query.AssetId.Value));
        }
        else
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullFieldName(query.FieldName), BsonType.Null));
        }
    }
}
