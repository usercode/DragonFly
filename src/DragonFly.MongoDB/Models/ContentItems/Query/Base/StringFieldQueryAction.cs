// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// StringFieldQueryAction
/// </summary>
public class StringFieldQueryAction : FieldQueryAction<StringFieldQuery>
{
    public override void Apply(StringFieldQuery query, FieldQueryActionContext context)
    {
        if (string.IsNullOrEmpty(query.Pattern) == false)
        {
            context.Filters.Add(
                query.PatternType switch
                {
                    StringQueryType.Equal => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Pattern),
                    StringQueryType.Contain => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}.*", "i")),
                    StringQueryType.StartWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($"^{query.Pattern}.*", "i")),
                    StringQueryType.EndWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}$", "i")),
                    _ => Builders<MongoContentItem>.Filter.Empty
                }
                );
        }
    }
}
