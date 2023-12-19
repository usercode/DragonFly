// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// AssetJsonFieldSerializer
/// </summary>
public class AssetJsonFieldSerializer : JsonFieldSerializer<AssetField>
{
    public override AssetField Read(SchemaField schemaField, JsonNode? jsonValue)
    {
        AssetField contentField = new AssetField();

        RestAsset? restAsset = jsonValue.Deserialize(ApiJsonSerializerContext.Default.RestAsset);

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
            return JsonSerializer.SerializeToNode(contentField.Asset.ToRest(), ApiJsonSerializerContext.Default.RestAsset);
        }
        else
        {
            return null;
        }
    }
}
