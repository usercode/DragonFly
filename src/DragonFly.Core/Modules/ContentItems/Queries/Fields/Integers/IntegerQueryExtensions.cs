// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IntegerQueryExtensions
/// </summary>
public static class IntegerQueryExtensions
{
    public static TContentQuery Integer<TContentQuery>(this TContentQuery query, string name, int? value, int? minValue = null, int? maxValue = null)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new IntegerQuery() { FieldName = name, Value = value, MinValue = minValue, MaxValue = maxValue });

        return query;
    }
}
