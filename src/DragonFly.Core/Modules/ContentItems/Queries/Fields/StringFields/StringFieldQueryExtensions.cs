// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// StringFieldQueryExtensions
/// </summary>
public static class StringFieldQueryExtensions
{
    public static TContentQuery StringQuery<TContentQuery>(this TContentQuery query, string name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new StringFieldQuery() { FieldName = name, Pattern = pattern, PatternType = type });

        return query;
    }
}
