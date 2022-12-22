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
    public static ContentQuery<TContentModel> AddStringQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, StringField>> name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
        where TContentModel : class, IContentModel
    {
        return query.AddStringQuery(ReflectionHelper.GetPropertyName(name), pattern, type);
    }

    public static ContentQuery<TContentModel> AddStringQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, string?>> name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
        where TContentModel : class, IContentModel
    {
        return query.AddStringQuery(ReflectionHelper.GetPropertyName(name), pattern, type);
    }    
}
