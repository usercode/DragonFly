// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static TContentQuery AddReferenceQuery<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : IContentQuery
    {
        query.Fields.Add(new ReferenceFieldQuery() { FieldName = name, ContentItemId = id });

        return query;
    }
}
