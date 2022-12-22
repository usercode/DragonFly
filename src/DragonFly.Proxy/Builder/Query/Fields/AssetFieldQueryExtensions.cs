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
    public static ContentQuery<TContentModel> AddAssetQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, AssetField>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AddAssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }

    public static ContentQuery<TContentModel> AddAssetQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, Asset?>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AddAssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }
}
