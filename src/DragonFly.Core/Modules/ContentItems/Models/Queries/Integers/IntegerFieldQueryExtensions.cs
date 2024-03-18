// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static TContentQuery Integer<TContentQuery>(this TContentQuery query, string field, long? value, NumberQueryType type = NumberQueryType.Equal)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new IntegerFieldQuery() { FieldName = field, Value = value, Type = type });

        return query;
    }
}
