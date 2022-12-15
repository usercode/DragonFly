// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// StringFieldQueryExtensions
/// </summary>
public static class StringFieldQueryExtensions
{
    public static IContentQuery<TModel> AddStringQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, StringField>> name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
    {
        query.AddStringQuery(ReflectionHelper.GetPropertyName(name), pattern, type);

        return query;
    }

    public static IContentQuery<TModel> AddStringQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, string?>> name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
    {
        query.AddStringQuery(ReflectionHelper.GetPropertyName(name), pattern, type);

        return query;
    }    
}
