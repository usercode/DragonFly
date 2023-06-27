// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static TContentQuery IntegerQuery<TContentQuery>(this TContentQuery query, string name, int? value, int? minValue = null, int? maxValue = null)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new IntegerFieldQuery() { FieldName = name, Value = value, MinValue = minValue, MaxValue = maxValue });

        return query;
    }
}
