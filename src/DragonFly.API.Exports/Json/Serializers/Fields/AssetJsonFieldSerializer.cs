// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;
using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.AspNetCore.API.Models.Assets;

namespace DragonFly.API.Json;

/// <summary>
/// AssetJsonFieldSerializer
/// </summary>
public class AssetJsonFieldSerializer : JsonFieldSerializer<AssetField>
{
    public override AssetField Read(SchemaField schemaField, JsonNode? jsonValue)
    {
        AssetField contentField = new AssetField();

        RestAsset? restAsset = jsonValue.Deserialize<RestAsset>(JsonSerializerDefault.Options);

        if (restAsset != null)
        {
            contentField.Asset = restAsset.ToModel();
        }

        return contentField;
    }

    public override JsonNode? Write(AssetField contentField, bool includeNavigationProperty)
    {
        if (contentField.Asset != null)
        {
            return JsonSerializer.SerializeToNode(contentField.Asset.ToRest(), JsonSerializerDefault.Options);
        }
        else
        {
            return null;
        }
    }
}
