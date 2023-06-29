// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetQueryExtensions
/// </summary>
public static class AssetQueryExtensions
{
    public static TContentQuery Asset<TContentQuery>(this TContentQuery query, string name, Guid? id)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new AssetQuery() { FieldName = name, AssetId = id });

        return query;
    }
}
