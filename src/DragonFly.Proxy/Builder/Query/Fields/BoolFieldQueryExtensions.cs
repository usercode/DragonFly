// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// BoolFieldQueryExtensions
/// </summary>
public static class BoolFieldQueryExtensions
{
    public static IContentQuery<TModel> AddBoolQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, BoolField>> name, bool? value)
    {
        query.AddBoolQuery(ReflectionHelper.GetPropertyName(name), value);

        return query;
    }

    public static IContentQuery<TModel> AddBoolQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, bool?>> name, bool? value)
    {
        query.AddBoolQuery(ReflectionHelper.GetPropertyName(name), value);

        return query;
    }
}
