using DragonFly.Content;
using DragonFly.Core.ContentItems.Queries.Fields;
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
            if (string.IsNullOrWhiteSpace(query.Pattern))
            {
                return;
            }

            context.Filters.Add(
                query.Type switch
                {
                    StringQueryType.Equal => Builders<MongoContentItem>.Filter.Eq($"{nameof(MongoContentItem.Fields)}.{query.FieldName}", query.Pattern),
                    StringQueryType.Contains => Builders<MongoContentItem>.Filter.Regex($"{nameof(MongoContentItem.Fields)}.{query.FieldName}", $".*{query.Pattern}.*"),
                    StringQueryType.StartsWith => Builders<MongoContentItem>.Filter.Regex($"{nameof(MongoContentItem.Fields)}.{query.FieldName}", $"^{query.Pattern}.*"),
                    StringQueryType.EndsWith => Builders<MongoContentItem>.Filter.Regex($"{nameof(MongoContentItem.Fields)}.{query.FieldName}", $".*{query.Pattern}$"),
                    _ => Builders<MongoContentItem>.Filter.Empty
                }
                );
        }
    }
}
