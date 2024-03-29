﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// IntegerFieldQueryAction
/// </summary>
public class IntegerFieldQueryAction : FieldQueryAction<IntegerFieldQuery>
{
    public override void Apply(IntegerFieldQuery query, FieldQueryActionContext context)
    {
        if (query.Value != null)
        {
            FilterDefinition<MongoContentItem> filter = query.Type switch
            {
                NumberQueryType.Equal => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Value.Value),
                NumberQueryType.LessThan => Builders<MongoContentItem>.Filter.Lt($"{CreateFullFieldName(query.FieldName)}", query.Value.Value),
                NumberQueryType.LessThanOrEqual => Builders<MongoContentItem>.Filter.Lte($"{CreateFullFieldName(query.FieldName)}", query.Value.Value),
                NumberQueryType.GreaterThan => Builders<MongoContentItem>.Filter.Gt($"{CreateFullFieldName(query.FieldName)}", query.Value.Value),
                NumberQueryType.GreaterThanOrEqual => Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.Value.Value),
                _ => throw new Exception()
            };

            context.Filters.Add(filter);
        }
    }
}
