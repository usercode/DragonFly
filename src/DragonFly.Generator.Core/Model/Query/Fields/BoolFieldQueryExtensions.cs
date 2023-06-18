// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;

namespace DragonFly.Query;

/// <summary>
/// BoolFieldQueryExtensions
/// </summary>
public static class BoolFieldQueryExtensions
{
    public static ContentQuery<TContentModel> BoolQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, BoolField>> name, bool? value)
        where TContentModel : class, IContentModel
    {
        return query.BoolQuery(ReflectionHelper.GetPropertyName(name), value);
    }

    public static ContentQuery<TContentModel> BoolQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, bool?>> name, bool? value)
        where TContentModel : class, IContentModel
    {
        return query.BoolQuery(ReflectionHelper.GetPropertyName(name), value);
    }
}
