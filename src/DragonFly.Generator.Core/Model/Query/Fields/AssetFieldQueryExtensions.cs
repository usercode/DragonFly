// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;

namespace DragonFly.Query;

/// <summary>
/// AssetFieldQueryExtensions
/// </summary>
public static class AssetFieldQueryExtensions
{
    public static ContentQuery<TContentModel> AssetQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, AssetField>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }

    public static ContentQuery<TContentModel> AssetQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, Asset?>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.AssetQuery(ReflectionHelper.GetPropertyName(name), id);
    }
}
