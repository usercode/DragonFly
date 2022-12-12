// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// BoolFieldQueryExtensions
/// </summary>
public static class BoolFieldQueryExtensions
{
    public static ContentItemQuery AddBoolQuery(this ContentItemQuery queryParameters, string name, bool? value)
    {
        queryParameters.Fields.Add(new BoolFieldQuery() { FieldName = name, Value = value});

        return queryParameters;
    }
}
