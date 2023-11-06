// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// ComponentJsonFieldSerializer
/// </summary>
public class ComponentJsonFieldSerializer : JsonFieldSerializer<ComponentField>
{
    public override ComponentField Read(SchemaField schemaField, JsonNode? jsonNode)
    {
        ComponentField contentField = new ComponentField();

        RestContentComponent? restContentItem = jsonNode.Deserialize<RestContentComponent>(ApiJsonSerializerDefault.Options);

        if (restContentItem == null)
        {
            throw new Exception();
        }

        contentField.ContentComponent = restContentItem.ToModel();

        return contentField;
    }

    public override JsonNode? Write(ComponentField contentField, bool includeNavigationProperty)
    {
        if (includeNavigationProperty && contentField.ContentComponent != null)
        {
            return JsonSerializer.SerializeToNode(contentField.ContentComponent.ToRest(true, true), ApiJsonSerializerDefault.Options);
        }
        else
        {
            return null;
        }
    }
}
