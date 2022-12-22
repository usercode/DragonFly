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
    public static ContentQuery<TContentModel> AddReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, ReferenceField>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AddReferenceQuery(ReflectionHelper.GetPropertyName(name), id);
    }

    public static ContentQuery<TContentModel> AddReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, object?>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AddReferenceQuery(ReflectionHelper.GetPropertyName(name), id);
    }
}
