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
    /// StringFieldQueryAction
    /// </summary>
    public class StringFieldQueryAction : FieldQueryAction<StringFieldQuery>
    {
        public override void Apply(StringFieldQuery query, FieldQueryActionContext context)
        {
            if (string.IsNullOrEmpty(query.Pattern) == false)
            {
                context.Filters.Add(
                    query.Type switch
                    {
                        StringFieldQueryType.Equals => Builders<MongoContentItem>.Filter.Eq($"{CreateFullFieldName(query.FieldName)}", query.Pattern),
                        StringFieldQueryType.Contains => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $".*{query.Pattern}.*"),
                        StringFieldQueryType.StartsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $"^{query.Pattern}.*"),
                        StringFieldQueryType.EndsWith => Builders<MongoContentItem>.Filter.Regex($"{CreateFullFieldName(query.FieldName)}", $".*{query.Pattern}$"),
                        _ => Builders<MongoContentItem>.Filter.Empty
                    }
                    );
            }
        }
    }
}
