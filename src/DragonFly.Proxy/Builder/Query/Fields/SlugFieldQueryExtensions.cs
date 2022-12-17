// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static IContentQuery<TModel> AddSlugQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, SlugField>> name, string value)
    {
        return query.AddSlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }

    public static IContentQuery<TModel> AddSlugQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, string?>> name, string value)
    {
        return query.AddSlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }
}
