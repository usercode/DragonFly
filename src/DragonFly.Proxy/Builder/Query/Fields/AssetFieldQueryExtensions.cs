// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

/// <summary>
/// AssetFieldQueryExtensions
/// </summary>
public static class AssetFieldQueryExtensions
{
    public static IContentQuery<TModel> AddAssetQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, AssetField>> name, Guid? id)
    {
        return query.AddAssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }

    public static IContentQuery<TModel> AddAssetQuery<TModel>(this IContentQuery<TModel> query, Expression<Func<TModel, Asset?>> name, Guid? id)
    {
        return query.AddAssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }
}
