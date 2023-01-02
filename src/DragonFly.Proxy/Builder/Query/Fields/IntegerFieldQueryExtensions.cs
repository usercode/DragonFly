// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy;
using DragonFly.Proxy.Helpers;

namespace DragonFly.Query;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static ContentQuery<TContentModel> AddIntegerQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, IntegerField>> name, int? value, int? minValue = null, int? maxValue = null)
        where TContentModel : class, IContentModel
    {
        return query.AddIntegerQuery(ReflectionHelper.GetPropertyName(name), value, minValue, maxValue);
    }

    public static ContentQuery<TContentModel> AddIntegerQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, long?>> name, int? value, int? minValue = null, int? maxValue = null)
        where TContentModel : class, IContentModel
    {
        return query.AddIntegerQuery(ReflectionHelper.GetPropertyName(name), value, minValue, maxValue);
    }
}
