// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static TContentQuery AddIntegerQuery<TContentQuery>(this TContentQuery query, string name, int? value, int? minValue = null, int? maxValue = null)
        where TContentQuery : IContentQuery
    {
        query.Fields.Add(new IntegerFieldQuery() { FieldName = name, Value = value, MinValue = minValue, MaxValue = maxValue });

        return query;
    }
}
