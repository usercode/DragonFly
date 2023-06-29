// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugQueryExtensions
/// </summary>
public static class SlugQueryExtensions
{
    public static TContentQuery Slug<TContentQuery>(this TContentQuery query, string name, string value)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new SlugQuery() { FieldName = name, Value = value });

        return query;
    }
}
