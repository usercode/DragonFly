// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// FloatFieldQueryAction
/// </summary>
public class FloatFieldQueryAction : FieldQueryAction<FloatFieldQuery>
{
    public override void Apply(FloatFieldQuery query, FieldQueryActionContext context)
    {
        if (query.Value != null)
        {
            var filter = query.Type switch
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
