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
    /// ReferenceFieldQueryAction
    /// </summary>
    public class ReferenceFieldQueryAction : FieldQueryAction<ReferenceFieldQuery>
    {
        public override void Apply(ReferenceFieldQuery query, FieldQueryActionContext context)
        {
            if (query.ContentItemId != null)
            {
                context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullReferenceFieldName(query.FieldName), query.ContentItemId.Value));
            }
        }
    }
}
