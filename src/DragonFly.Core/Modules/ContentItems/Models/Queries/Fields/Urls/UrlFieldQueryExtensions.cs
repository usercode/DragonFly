// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// UrlFieldQueryExtensions
/// </summary>
public static class UrlFieldQueryExtensions
{
    public static TContentQuery Url<TContentQuery>(this TContentQuery query, string field, string value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new UrlFieldQuery() { FieldName = field, Value = value });

        return query;
    }
}
