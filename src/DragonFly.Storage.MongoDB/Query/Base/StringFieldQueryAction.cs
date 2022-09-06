using DragonFly.Models;
using DragonFly.Query;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query.Base;

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
                    StringFieldQueryType.Equals => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Pattern),
                    StringFieldQueryType.Contains => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}.*", "i")),
                    StringFieldQueryType.StartsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($"^{query.Pattern}.*", "i")),
                    StringFieldQueryType.EndsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", new BsonRegularExpression($".*{query.Pattern}$", "i")),
                    _ => Builders<MongoContentItem>.Filter.Empty
                }
                );
        }
    }
}
