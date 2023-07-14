// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BoolFieldQueryExtensions
/// </summary>
public static class BoolFieldQueryExtensions
{
    public static TContentQuery Bool<TContentQuery>(this TContentQuery query, string field, bool? value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new BoolFieldQuery() { FieldName = field, Value = value});

        return query;
    }
}
