// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static TContentQuery Slug<TContentQuery>(this TContentQuery query, string field, string value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new SlugFieldQuery() { FieldName = field, Value = value });

        return query;
    }
}
