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
    public static ContentQuery<TContentModel> AddBoolQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, BoolField>> name, bool? value)
        where TContentModel : class, IContentModel
    {
        return query.AddBoolQuery(ReflectionHelper.GetPropertyName(name), value);
    }

    public static ContentQuery<TContentModel> AddBoolQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, bool?>> name, bool? value)
        where TContentModel : class, IContentModel
    {
        return query.AddBoolQuery(ReflectionHelper.GetPropertyName(name), value);
    }
}
