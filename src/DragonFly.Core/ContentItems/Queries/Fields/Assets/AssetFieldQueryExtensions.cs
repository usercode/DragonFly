// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// AssetFieldQueryExtensions
/// </summary>
public static class AssetFieldQueryExtensions
{
    public static TContentQuery AddAssetQuery<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : IContentQuery
    {
        query.Fields.Add(new AssetFieldQuery() { FieldName = name, AssetId = id });

        return query;
    }
}
