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
    public static ContentQuery<TContentModel> AddSlugQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, SlugField>> name, string value)
        where TContentModel : class, IContentModel
    {
        return query.AddSlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }

    public static ContentQuery<TContentModel> AddSlugQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, string?>> name, string value)
        where TContentModel : class, IContentModel
    {
        return query.AddSlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }
}
