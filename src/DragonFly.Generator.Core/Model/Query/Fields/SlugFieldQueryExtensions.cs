// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;

namespace DragonFly.Query;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static ContentQuery<TContentModel> SlugQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, SlugField>> name, string value)
        where TContentModel : class, IContentModel
    {
        return query.SlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }

    public static ContentQuery<TContentModel> SlugQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, string?>> name, string value)
        where TContentModel : class, IContentModel
    {
        return query.SlugQuery(ReflectionHelper.GetPropertyName(name), value);
    }
}
