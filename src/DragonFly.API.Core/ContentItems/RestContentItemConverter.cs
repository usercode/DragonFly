// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DragonFly.API;

public static class RestContentItemConverter
{
    public static ContentItem ToModel(this RestContentItem restContentItem)
    {
        ContentSchema schema;

        if (restContentItem.Schema is JsonValue jsonValue)
        {
            schema = new ContentSchema(jsonValue.GetValue<string>());
        }
        else
        {
            RestContentSchema? restSchema = restContentItem.Schema.Deserialize<RestContentSchema>(ApiJsonSerializerDefault.Options);
                
            if (restSchema == null)
            {
                throw new Exception("Unknown schema");
            }

            schema = restSchema.ToModel();
        }

        ContentItem contentItem = schema.CreateContent();

        contentItem.Id = restContentItem.Id;
        contentItem.CreatedAt = restContentItem.CreatedAt;
        contentItem.ModifiedAt = restContentItem.ModifiedAt;
        contentItem.PublishedAt = restContentItem.PublishedAt;
        contentItem.Version = restContentItem.Version;
        contentItem.SchemaVersion = restContentItem.SchemaVersion;
        contentItem.ValidationState = restContentItem.ValidationContext;

        foreach (var restField in restContentItem.Fields)
        {
            restField.Value.ToModelValue(restField.Key, contentItem, schema);
        }

        return contentItem;
    }

    public static ContentComponent ToModel(this RestContentComponent restContentItem)
    {
        ContentSchema? schema;

        if (restContentItem.Schema is JsonValue jsonValue)
        {
            schema = new ContentSchema(jsonValue.GetValue<string>());
        }
        else
        {
            RestContentSchema? restSchema = restContentItem.Schema.Deserialize<RestContentSchema>(ApiJsonSerializerDefault.Options);

            if (restSchema == null)
            {
                throw new Exception("Unknown schema");
            }

            schema = restSchema.ToModel();
        }

        ContentComponent contentItem = schema.CreateEmbeddedContent();

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
            restContentItem.Schema = JsonSerializer.SerializeToNode(contentItem.Schema.ToRest(), ApiJsonSerializerDefault.Options);
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
        restContentItem.ValidationContext = contentItem.ValidationState;

        foreach (var field in contentItem.Fields)
        {
            JsonNode? node = field.Value.ToRestValue(includeNavigationProperties);

            //if (node != null)
            //{
                restContentItem.Fields.Add(field.Key, node);
            //}
        }

        return restContentItem;
    }

    public static RestContentComponent ToRest(this ContentComponent contentItem, bool includeSchema, bool includeNavigationProperties)
    {
        RestContentComponent restContentItem = new RestContentComponent();

        if (includeSchema)
        {
            restContentItem.Schema = JsonSerializer.SerializeToNode(contentItem.Schema.ToRest(), ApiJsonSerializerDefault.Options);
        }
        else
        {
            restContentItem.Schema = JsonValue.Create(contentItem.Schema.Name);
        }

        foreach (var field in contentItem.Fields)
        {
            JsonNode? node = field.Value.ToRestValue(includeNavigationProperties);

            restContentItem.Fields[field.Key] = node;
        }

        return restContentItem;
    }
}
