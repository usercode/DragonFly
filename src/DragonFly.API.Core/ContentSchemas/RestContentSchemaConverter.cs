// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;

namespace DragonFly.API;

public static class RestContentSchemaConverter
{
    public static ContentSchema ToModel(this RestContentSchema restSchema)
    {
        ContentSchema schema = new ContentSchema(restSchema.Name);

        schema.Id = restSchema.Id;
        schema.CreatedAt = restSchema.CreatedAt;
        schema.ModifiedAt = restSchema.ModifiedAt;
        schema.Version = restSchema.Version;
        schema.ListFields = restSchema.ListFields.ToList();
        schema.ReferenceFields = restSchema.ReferenceFields.ToList();
        schema.QueryFields = restSchema.QueryFields.ToList();
        schema.OrderFields = restSchema.OrderFields.ToList();
        schema.Preview = restSchema.Preview;

        foreach (var mongoField in restSchema.Fields)
        {
            schema.Fields.Add(mongoField.Key, mongoField.Value.ToModel());
        }

        return schema;
    }

    public static RestContentSchema ToRest(this ContentSchema schema)
    {
        RestContentSchema restSchema = new RestContentSchema();

        restSchema.Id = schema.Id;
        restSchema.Name = schema.Name;
        restSchema.CreatedAt = schema.CreatedAt;
        restSchema.ModifiedAt = schema.ModifiedAt;
        restSchema.Version = schema.Version;
        restSchema.ListFields = schema.ListFields.ToList();
        restSchema.ReferenceFields = schema.ReferenceFields.ToList();
        restSchema.QueryFields = schema.QueryFields.ToList();
        restSchema.OrderFields = schema.OrderFields.ToList();
        restSchema.Preview = schema.Preview;

        foreach (var field in schema.Fields)
        {
            restSchema.Fields.Add(field.Key, field.Value.ToRest());
        }

        return restSchema;
    }

    public static RestContentSchemaField ToRest(this SchemaField schemaField)
    {
        RestContentSchemaField restContentFieldDefinition = new RestContentSchemaField();
        restContentFieldDefinition.Label = schemaField.Label;
        restContentFieldDefinition.SortKey = schemaField.SortKey;
        restContentFieldDefinition.FieldType = schemaField.FieldType;

        if (schemaField.Options != null)
        {
            restContentFieldDefinition.Options = JsonSerializer.SerializeToNode(schemaField.Options, schemaField.Options.GetType(), ApiJsonSerializerDefault.Options);
        }

        return restContentFieldDefinition;
    }

    public static SchemaField ToModel(this RestContentSchemaField definition)
    {
        Type? optionsType = FieldManager.Default.GetOptionsType(definition.FieldType);

        FieldOptions? options = null;

        if (optionsType != null)
        {
            options = (FieldOptions?)definition.Options.Deserialize(optionsType, ApiJsonSerializerDefault.Options);
        }

        SchemaField schemaField = new SchemaField(definition.FieldType, options);
        schemaField.Label = definition.Label;
        schemaField.SortKey = definition.SortKey;

        return schemaField;
    }
}
