// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// ReferenceFieldQueryAction
/// </summary>
public class ReferenceFieldQueryAction : FieldQueryAction<ReferenceFieldQuery>
{
    public override void Apply(ReferenceFieldQuery query, FieldQueryActionContext context)
    {
        if (query.ContentItemId != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullReferenceFieldName(query.FieldName), query.ContentItemId.Value));
        }
        else
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullReferenceFieldName(query.FieldName), BsonType.Null));
        }
    }
}
