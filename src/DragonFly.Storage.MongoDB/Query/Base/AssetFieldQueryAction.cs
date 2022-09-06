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
/// AssetFieldQueryAction
/// </summary>
public class AssetFieldQueryAction : FieldQueryAction<AssetFieldQuery>
{
    public override void Apply(AssetFieldQuery query, FieldQueryActionContext context)
    {
        if (query.AssetId != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullFieldName(query.FieldName), query.AssetId.Value));
        }
        else
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Eq(CreateFullFieldName(query.FieldName), BsonType.Null));
        }
    }
}
