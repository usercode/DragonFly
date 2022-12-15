// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static IContentQuery<TModel> AddReferenceQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, ReferenceField>> name, Guid? id)
    {
        query.AddReferenceQuery(ReflectionHelper.GetPropertyName(name), id);

        return query;
    }

    public static IContentQuery<TModel> AddReferenceQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, object?>> name, Guid? id)
    {
        query.AddReferenceQuery(ReflectionHelper.GetPropertyName(name), id);

        return query;
    }
}
