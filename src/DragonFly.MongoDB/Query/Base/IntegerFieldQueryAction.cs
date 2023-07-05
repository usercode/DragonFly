// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// IntegerFieldQueryAction
/// </summary>
public class IntegerFieldQueryAction : FieldQueryAction<IntegerFieldQuery>
{
    public override void Apply(IntegerFieldQuery query, FieldQueryActionContext context)
    {
        if (query.MinValue != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.MinValue.Value));
        }

        if (query.MaxValue != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Lte($"{CreateFullFieldName(query.FieldName)}", query.MaxValue.Value));
        }

        if (query.Value != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.Value.Value));
        }
    }
}
