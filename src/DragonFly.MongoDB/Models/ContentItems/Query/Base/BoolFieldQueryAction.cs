﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// BoolFieldQueryAction
/// </summary>
public class BoolFieldQueryAction : FieldQueryAction<BoolFieldQuery>
{
    public override void Apply(BoolFieldQuery query, FieldQueryActionContext context)
    {
        if (query.Value != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Value.Value));
        }
    }
}
