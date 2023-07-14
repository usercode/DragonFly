// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static TContentQuery Reference<TContentQuery>(this TContentQuery query, string field, Guid? id)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new ReferenceFieldQuery() { FieldName = field, ContentItemId = id });

        return query;
    }
}
