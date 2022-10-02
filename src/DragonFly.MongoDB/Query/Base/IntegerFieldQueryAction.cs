// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Models;
using DragonFly.Query;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB.Query;

/// <summary>
/// IntegerFieldQueryAction
/// </summary>
public class IntegerFieldQueryAction : FieldQueryAction<IntegerFieldQuery>
{
    public override void Apply(IntegerFieldQuery query, FieldQueryActionContext context)
    {
        if (query.MinValue != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.MinValue.Value));
        }

        if (query.MaxValue != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Lte($"{CreateFullFieldName(query.FieldName)}", query.MaxValue.Value));
        }

        if (query.Value != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter.Gte($"{CreateFullFieldName(query.FieldName)}", query.Value.Value));
        }
    }
}
