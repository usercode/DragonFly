// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetFieldQueryExtensions
/// </summary>
public static class AssetFieldQueryExtensions
{
    public static TContentQuery Asset<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new AssetFieldQuery() { FieldName = name, AssetId = id });

        return query;
    }
}
