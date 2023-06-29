// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// StringFieldQueryAction
/// </summary>
public class StringFieldQueryAction : FieldQueryAction<StringQuery>
{
    public override void Apply(StringQuery query, FieldQueryActionContext context)
    {
        if (string.IsNullOrEmpty(query.Pattern) == false)
        {
            context.Filters.Add(
                query.PatternType switch
                {
                    StringQueryType.Equals => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Pattern),
                    StringQueryType.Contains => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}.*", "i")),
                    StringQueryType.StartsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($"^{query.Pattern}.*", "i")),
                    StringQueryType.EndsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}$", "i")),
                    _ => Builders<MongoContentItem>.Filter.Empty
                }
                );
        }
    }
}
