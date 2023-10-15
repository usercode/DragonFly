// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FloatFieldQueryExtensions
/// </summary>
public static class FloatFieldQueryExtensions
{
    public static TContentQuery Float<TContentQuery>(this TContentQuery query, string field, double? value, NumberQueryType type = NumberQueryType.Equal)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new FloatFieldQuery() { FieldName = field, Value = value, Type = type });

        return query;
    }
}
