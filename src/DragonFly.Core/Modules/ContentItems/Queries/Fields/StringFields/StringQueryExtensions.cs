// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// StringQueryExtensions
/// </summary>
public static class StringQueryExtensions
{
    public static TContentQuery String<TContentQuery>(this TContentQuery query, string name, string pattern, StringQueryType type = StringQueryType.Equals)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new StringQuery() { FieldName = name, Pattern = pattern, PatternType = type });

        return query;
    }
}
