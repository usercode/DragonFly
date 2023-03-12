// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using DragonFly.Proxy;
using DragonFly.Proxy.Helpers;
using DragonFly.Query;

namespace DragonFly.Query;

public static class QueryExtensions
{
    public static ContentQuery<TContentModel> OrderBy<TContentModel>(this ContentQuery<TContentModel> query, Expression<Func<TContentModel, string>> field, bool asc = true)
        where TContentModel : class, IContentModel
    {
        query.OrderFields.Add(new FieldOrder($"Fields.{ReflectionHelper.GetPropertyName(field)}", asc));

        return query;
    }
}
