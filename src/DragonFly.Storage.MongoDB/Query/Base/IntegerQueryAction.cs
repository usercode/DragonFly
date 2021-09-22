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
    public class IntegerQueryAction : QueryAction<IntegerQuery>
    {
        public override void Apply(IntegerQuery query, QueryActionContext context)
        {
            if (query.MinValue != null)
            {
                context.Filters.Add(Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.MinValue.Value));
            }

            if (query.MaxValue != null)
            {
                context.Filters.Add(Builders<MongoContentItem>.Filter.Lte($"{CreateFullFieldName(query.FieldName)}", query.MaxValue.Value));
            }          
        }
    }
}
