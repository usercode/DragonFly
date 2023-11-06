// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// ReferenceJsonFieldSerializer
/// </summary>
public class ReferenceJsonFieldSerializer : JsonFieldSerializer<ReferenceField>
{
    public override ReferenceField Read(SchemaField schemaField, JsonNode? jsonNode)
    {
        ReferenceField contentField = new ReferenceField();

        RestContentItem? restContentItem = jsonNode.Deserialize<RestContentItem>(ApiJsonSerializerDefault.Options);

        if (restContentItem != null)
        {
            contentField.ContentItem = restContentItem.ToModel();
        }

        return contentField;
    }

    public override JsonNode? Write(ReferenceField contentField, bool includeNavigationProperty)
    {
        if (includeNavigationProperty && contentField.ContentItem != null)
        {
            return JsonSerializer.SerializeToNode(contentField.ContentItem.ToRest(true, false), ApiJsonSerializerDefault.Options);            
        }
        else
        {
            return null;
        }
    }
}
