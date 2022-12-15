// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static IContentQuery<TModel> AddIntegerQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, long?>> name, int? value, int? minValue = null, int? maxValue = null)
    {
        query.AddIntegerQuery(ReflectionHelper.GetPropertyName(name), value, minValue, maxValue);

        return query;
    }
}
