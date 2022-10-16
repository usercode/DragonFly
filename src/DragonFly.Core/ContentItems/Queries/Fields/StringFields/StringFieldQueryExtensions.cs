// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// StringFieldQueryExtensions
/// </summary>
public static class StringFieldQueryExtensions
{
    public static ContentItemQuery AddStringQuery(this ContentItemQuery queryParameters, string name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
    {
        queryParameters.Fields.Add(new StringFieldQuery() { FieldName = name, Pattern = pattern, PatternType = type });

        return queryParameters;
    }
}
