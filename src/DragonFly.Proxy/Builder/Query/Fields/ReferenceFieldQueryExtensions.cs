﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy;
using DragonFly.Proxy.Helpers;

namespace DragonFly.Query;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static ContentQuery<TContentModel> ReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, ReferenceField>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.Reference(ReflectionHelper.GetPropertyName(name), id);
    }

    public static ContentQuery<TContentModel> ReferenceQuery<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, object?>> name, Guid? id)
        where TContentModel : class, IContentModel
    {
        return query.Reference(ReflectionHelper.GetPropertyName(name), id);
    }
}
