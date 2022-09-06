using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.Data.Models;

public static class RestContentItemConverter
{
    public static ContentItem ToModel(this RestContentItem restContentItem)
    {
        return ToModel(restContentItem, null);
    }
    
    public static ContentItem ToModel(this RestContentItem restContentItem, ContentSchema? schema)
    {
        if (schema == null)
        {
            if (restContentItem.Schema is JsonValue jsonValue)
            {
                schema = new ContentSchema(jsonValue.GetValue<string>());
            }
            else
            {
                RestContentSchema? restSchema = restContentItem.Schema.Deserialize<RestContentSchema>();
                
                if (restSchema != null)
                {
                    schema = restSchema.ToModel();
                }
            }
        }

        ContentItem contentItem = schema.CreateContentItem();

        contentItem.Id = restContentItem.Id;
        contentItem.CreatedAt = restContentItem.CreatedAt;
        contentItem.ModifiedAt = restContentItem.ModifiedAt;
        contentItem.PublishedAt = restContentItem.PublishedAt;
        contentItem.Version = restContentItem.Version;
        contentItem.SchemaVersion = restContentItem.SchemaVersion;

        foreach (var restField in restContentItem.Fields)
        {
            restField.Value.ToModelValue(restField.Key, contentItem, schema);
        }

        return contentItem;
    }

    public static ContentEmbedded ToModel(this RestContentEmbedded restContentItem)
    {
        ContentSchema schema = restContentItem.Schema.ToModel();

        ContentEmbedded contentItem = schema.CreateContentEmbedded();

        foreach (var restField in restContentItem.Fields)
        {
            restField.Value.ToModelValue(restField.Key, contentItem, schema);
        }

        return contentItem;
    }

    public static RestContentItem ToRest(this ContentItem contentItem, bool includeSchema = true, bool includeNavigationProperties = true)
    {
        RestContentItem restContentItem = new RestContentItem();

        restContentItem.Id = contentItem.Id;
        if (includeSchema)
        {
            restContentItem.Schema = JsonSerializer.SerializeToNode(contentItem.Schema.ToRest());
        }
        else
        {
            restContentItem.Schema = JsonValue.Create(contentItem.Schema.Name);
        }
        restContentItem.CreatedAt = contentItem.CreatedAt;
        restContentItem.ModifiedAt = contentItem.ModifiedAt;
        restContentItem.PublishedAt = contentItem.PublishedAt;
        restContentItem.Version = contentItem.Version;
        restContentItem.SchemaVersion = contentItem.SchemaVersion;

        foreach (var field in contentItem.Fields)
        {
            JsonNode? node = field.Value.ToRestValue(includeNavigationProperties);

            if (node != null)
            {
                restContentItem.Fields.Add(field.Key, node);
            }
        }

        return restContentItem;
    }

    public static RestContentItem ToRest(this ContentEmbedded contentItem)
    {
        RestContentItem restContentItem = new RestContentItem();
        restContentItem.Schema = JsonSerializer.SerializeToNode(contentItem.Schema.ToRest());

        foreach (var field in contentItem.Fields)
        {
            restContentItem.Fields.Add(field.Key, field.Value.ToRestValue());
        }

        return restContentItem;
    }
}
