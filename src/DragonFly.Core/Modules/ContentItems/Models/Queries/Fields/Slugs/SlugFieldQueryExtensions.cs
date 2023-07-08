// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static TContentQuery Slug<TContentQuery>(this TContentQuery query, string name, string value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new SlugFieldQuery() { FieldName = name, Value = value });

        return query;
    }
}
