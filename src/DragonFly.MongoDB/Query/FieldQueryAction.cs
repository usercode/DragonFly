// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.MongoDB;

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
