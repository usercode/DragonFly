using DragonFly.Core.ContentItems.Queries.Fields;
using DragonFly.Core.ContentItems.Queries.Fields.Integers;
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
            context.Filters.Add(
               query.Operator switch
               {
                   IntegerQueryOperator.Equal => Builders<MongoContentItem>.Filter.Eq(query.FieldName, query.Value),
                   IntegerQueryOperator.LessThan => Builders<MongoContentItem>.Filter.Lt(query.FieldName, query.Value),
                   IntegerQueryOperator.LessThanOrEqual => Builders<MongoContentItem>.Filter.Lte(query.FieldName, query.Value),
                   IntegerQueryOperator.GreaterThan => Builders<MongoContentItem>.Filter.Gt(query.FieldName, query.Value),
                   IntegerQueryOperator.GreaterThanOrEqual => Builders<MongoContentItem>.Filter.Gte(query.FieldName, query.Value),
                   _ => Builders<MongoContentItem>.Filter.Empty
               }
               );
        }
    }
}
