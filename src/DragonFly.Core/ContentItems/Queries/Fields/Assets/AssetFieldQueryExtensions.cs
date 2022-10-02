// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Query;

/// <summary>
/// AssetFieldQueryExtensions
/// </summary>
public static class AssetFieldQueryExtensions
{
    public static ContentItemQuery AddAssetQuery(this ContentItemQuery queryParameters, string name, Guid? id)
    {
        queryParameters.Fields.Add(new AssetFieldQuery() { FieldName = name, AssetId = id });

        return queryParameters;
    }
}
