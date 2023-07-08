// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static TContentQuery Reference<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new ReferenceFieldQuery() { FieldName = name, ContentItemId = id });

        return query;
    }
}
