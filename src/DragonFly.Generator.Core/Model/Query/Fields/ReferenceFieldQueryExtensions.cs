// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;

namespace DragonFly.Query;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static ContentQuery<TContentModel> ReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, ReferenceField>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.ReferenceQuery(ReflectionHelper.GetPropertyName(name), id);
    }

    public static ContentQuery<TContentModel> ReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, object?>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.ReferenceQuery(ReflectionHelper.GetPropertyName(name), id);
    }
}
