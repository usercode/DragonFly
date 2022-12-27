// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// SlugFieldQueryExtensions
/// </summary>
public static class SlugFieldQueryExtensions
{
    public static TContentQuery AddSlugQuery<TContentQuery>(this TContentQuery query, string name, string value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new SlugFieldQuery() { FieldName = name, Value = value });

        return query;
    }
}
