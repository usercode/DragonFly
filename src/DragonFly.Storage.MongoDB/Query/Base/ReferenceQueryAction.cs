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
    public class ReferenceQueryAction : QueryAction<ReferenceQuery>
    {
        public override void Apply(ReferenceQuery query, QueryActionContext context)
        {
            if (query.ContentItemId != null)
            {
                context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullReferenceFieldName(query.FieldName), query.ContentItemId.Value));
            }
        }
    }
}
