// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;
using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.Data.Models;
using DragonFly.Models;

namespace DragonFly.API.Json;

/// <summary>
/// ReferenceJsonFieldSerializer
/// </summary>
public class ReferenceJsonFieldSerializer : JsonFieldSerializer<ReferenceField>
{
    public override ReferenceField Read(SchemaField schemaField, JsonNode? jsonNode)
    {
        ReferenceField contentField = new ReferenceField();

        RestContentItem? restContentItem = jsonNode.Deserialize<RestContentItem>(JsonSerializerDefault.Options);

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
            return JsonSerializer.SerializeToNode(contentField.ContentItem.ToRest(true, false), JsonSerializerDefault.Options);            
        }
        else
        {
            return null;
        }
    }
}
