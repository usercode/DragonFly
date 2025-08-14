// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// StringQueryExtensions
/// </summary>
public static class StringFieldQueryExtensions
{
    public static TContentQuery String<TContentQuery>(this TContentQuery query, string field, string pattern, StringQueryType type = StringQueryType.Equal)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new StringFieldQuery() { FieldName = field, Pattern = pattern, PatternType = type });

        return query;
    }
}
