// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// IntegerFieldQueryExtensions
/// </summary>
public static class IntegerFieldQueryExtensions
{
    public static ContentItemQuery AddIntegerQuery(this ContentItemQuery queryParameters, string name, int? value, int? minValue = null, int? maxValue = null)
    {
        queryParameters.Fields.Add(new IntegerFieldQuery() { FieldName = name, Value = value, MinValue = minValue, MaxValue = maxValue });

        return queryParameters;
    }
}
