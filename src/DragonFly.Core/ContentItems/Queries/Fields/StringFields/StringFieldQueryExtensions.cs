// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// StringFieldQueryExtensions
/// </summary>
public static class StringFieldQueryExtensions
{
    public static TContentQuery AddStringQuery<TContentQuery>(this TContentQuery query, string name, string pattern, StringFieldQueryType type = StringFieldQueryType.Equals)
        where TContentQuery : IContentQuery
    {
        query.Fields.Add(new StringFieldQuery() { FieldName = name, Pattern = pattern, PatternType = type });

        return query;
    }
}
