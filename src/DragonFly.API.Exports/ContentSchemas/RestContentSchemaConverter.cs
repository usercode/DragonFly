// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.AspNetCore.API.Models;
using System.Text.Json;

namespace DragonFly.Data.Models;

public static class RestContentSchemaConverter
{
    public static ContentSchema ToModel(this RestContentSchema restContentItem)
    {
        ContentSchema contentSchema = new ContentSchema(restContentItem.Name);

        contentSchema.Id = restContentItem.Id;
        contentSchema.CreatedAt = restContentItem.CreatedAt;
        contentSchema.ModifiedAt = restContentItem.ModifiedAt;
        contentSchema.Version = restContentItem.Version;
        contentSchema.ListFields = restContentItem.ListFields.ToList();
        contentSchema.ReferenceFields = restContentItem.ReferenceFields.ToList();
        contentSchema.QueryFields = restContentItem.QueryFields.ToList();
        contentSchema.OrderFields = restContentItem.OrderFields.ToList();

        foreach (var mongoField in restContentItem.Fields)
        {
            contentSchema.Fields.Add(mongoField.Key, mongoField.Value.ToModel());
        }

        return contentSchema;
    }

    public static RestContentSchema ToRest(this ContentSchema contentSchema)
    {
        RestContentSchema restContentItem = new RestContentSchema();

        restContentItem.Id = contentSchema.Id;
        restContentItem.Name = contentSchema.Name;
        restContentItem.CreatedAt = contentSchema.CreatedAt;
        restContentItem.ModifiedAt = contentSchema.ModifiedAt;
        restContentItem.Version = contentSchema.Version;
        restContentItem.ListFields = contentSchema.ListFields.ToList();
        restContentItem.ReferenceFields = contentSchema.ReferenceFields.ToList();
        restContentItem.QueryFields = contentSchema.QueryFields.ToList();
        restContentItem.OrderFields = contentSchema.OrderFields.ToList();

        foreach (var field in contentSchema.Fields)
        {
            restContentItem.Fields.Add(field.Key, field.Value.ToRest());
        }

        return restContentItem;
    }

    public static RestContentSchemaField ToRest(this SchemaField schemaField)
    {
        RestContentSchemaField restContentFieldDefinition = new RestContentSchemaField();
        restContentFieldDefinition.Label = schemaField.Label;
        restContentFieldDefinition.SortKey = schemaField.SortKey;
        restContentFieldDefinition.FieldType = schemaField.FieldType;

        if (schemaField.Options != null)
        {
            restContentFieldDefinition.Options = JsonSerializer.SerializeToNode(schemaField.Options, schemaField.Options.GetType(), JsonSerializerDefault.Options);
        }

        return restContentFieldDefinition;
    }

    public static SchemaField ToModel(this RestContentSchemaField definition)
    {
        Type? optionsType = ContentFieldManager.Default.GetOptionsType(definition.FieldType);

        ContentFieldOptions? options = null;

        if (optionsType != null)
        {
            options = (ContentFieldOptions?)definition.Options.Deserialize(optionsType, JsonSerializerDefault.Options);
        }

        SchemaField schemaField = new SchemaField(definition.FieldType, options);
        schemaField.Label = definition.Label;
        schemaField.SortKey = definition.SortKey;

        return schemaField;
    }
}
