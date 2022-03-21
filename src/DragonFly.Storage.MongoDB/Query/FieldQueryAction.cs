using DragonFly.Content;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query;

/// <summary>
/// FieldQueryAction
/// </summary>
/// <typeparam name="TQuery"></typeparam>
public class FieldQueryAction<TQuery> : IFieldQueryAction
    where TQuery : FieldQuery
{
    public virtual void Apply(TQuery query, FieldQueryActionContext context)
    {

    }

    void IFieldQueryAction.Apply(FieldQuery query, FieldQueryActionContext context)
    {
        Apply((TQuery)query, context);
    }

    protected string CreateFullFieldName(string? name)
    {
        return $"{nameof(MongoContentItem.Fields)}.{name}";
    }

    protected string CreateFullReferenceFieldName(string? name)
    {
        return $"{nameof(MongoContentItem.Fields)}.{name}.Id";
    }
}
