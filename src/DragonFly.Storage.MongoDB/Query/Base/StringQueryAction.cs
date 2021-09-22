using DragonFly.Content;
using DragonFly.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query.Base
{
    /// <summary>
    /// StringQueryAction
    /// </summary>
    public class StringQueryAction : QueryAction<StringQuery>
    {
        public override void Apply(StringQuery query, QueryActionContext context)
        {
            context.Filters.Add(
                query.Type switch
                {
                    StringQueryType.Equals => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Pattern),
                    StringQueryType.Contains => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $".*{query.Pattern}.*"),
                    StringQueryType.StartsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $"^{query.Pattern}.*"),
                    StringQueryType.EndsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $".*{query.Pattern}$"),
                    _ => Builders<MongoContentItem>.Filter.Empty
                }
                );
        }
    }
}
