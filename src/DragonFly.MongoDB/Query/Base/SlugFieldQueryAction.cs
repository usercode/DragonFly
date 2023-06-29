// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// SlugFieldQueryAction
/// </summary>
public class SlugFieldQueryAction : FieldQueryAction<SlugQuery>
{
    public override void Apply(SlugQuery query, FieldQueryActionContext context)
    {
        if (string.IsNullOrEmpty(query.Value) == false)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullFieldName(query.FieldName), query.Value));
        }
    }
}
