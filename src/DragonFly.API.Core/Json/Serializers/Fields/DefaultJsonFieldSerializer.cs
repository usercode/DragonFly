// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// DefaultJsonFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public class DefaultJsonFieldSerializer<TContentField> : JsonFieldSerializer<TContentField>
    where TContentField : ContentField, new()
{
    public override TContentField Read(SchemaField schemaField, JsonNode? jsonNode)
    {
        TContentField? result = JsonSerializer.Deserialize<TContentField>(jsonNode, ApiJsonSerializerDefault.Options);

        if (result != null)
        {
            return result;
        }

        return new TContentField();
    }

    public override JsonNode? Write(TContentField contentField, bool includeNavigationProperty)
    {
        return JsonSerializer.SerializeToNode(contentField, contentField.GetType(), ApiJsonSerializerDefault.Options);
    }
}
