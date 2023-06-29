// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceQueryExtensions
/// </summary>
public static class ReferenceQueryExtensions
{
    public static TContentQuery Reference<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new ReferenceQuery() { FieldName = name, ContentItemId = id });

        return query;
    }
}
