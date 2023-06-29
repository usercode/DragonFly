// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BoolQueryExtensions
/// </summary>
public static class BoolQueryExtensions
{
    public static TContentQuery Bool<TContentQuery>(this TContentQuery query, string name, bool? value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new BoolQuery() { FieldName = name, Value = value});

        return query;
    }
}
