// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static TContentQuery Integer<TContentQuery>(this TContentQuery query, string field, int? value, int? minValue = null, int? maxValue = null)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new IntegerFieldQuery() { FieldName = field, Value = value, MinValue = minValue, MaxValue = maxValue });

        return query;
    }
}
