// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// BoolFieldQueryExtensions
/// </summary>
public static class BoolFieldQueryExtensions
{
    public static TContentQuery BoolQuery<TContentQuery>(this TContentQuery query, string name, bool? value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new BoolFieldQuery() { FieldName = name, Value = value});

        return query;
    }
}
