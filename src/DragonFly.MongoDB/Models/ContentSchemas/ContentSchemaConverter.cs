﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace DragonFly.MongoDB;

static class ContentSchemaConverter
{
    public static ContentSchema ToModel(this MongoContentSchema mongoSchema)
    {
        ContentSchema schema = new ContentSchema(mongoSchema.Name);

        schema.Id = mongoSchema.Id;
        schema.CreatedAt = mongoSchema.CreatedAt;
        schema.ModifiedAt = mongoSchema.ModifiedAt;
        schema.Version = mongoSchema.Version;
        schema.ListFields = mongoSchema.ListFields.ToList();
        schema.ReferenceFields = mongoSchema.ReferenceFields.ToList();
        schema.QueryFields = mongoSchema.QueryFields.ToList();
        schema.OrderFields = mongoSchema.OrderFields.ToList();
        schema.Preview = mongoSchema.Preview;

        foreach (var field in mongoSchema.Fields)
        {
            Type? optionsType = FieldManager.Default.GetOptionsType(field.Value.FieldType);
            FieldOptions? options = null;

            if (field.Value.Options is BsonDocument bsonOptions)
            {
                options = (FieldOptions)BsonSerializer.Deserialize(bsonOptions, optionsType);

                //add missing options inside ArrayFieldOptions
                if (options is ArrayFieldOptions arrayFieldOptions)
                {
                    foreach (var f in arrayFieldOptions.Fields)
                    {
                        if (f.Value.Options == null)
                        {
                            f.Value.Options = FieldManager.Default.CreateOptions(f.Value.FieldType);
                        }
                    }
                }
            }

            if (options == null)
            {
                options = FieldManager.Default.CreateOptions(field.Value.FieldType);
            }

            SchemaField schemaField = new SchemaField(field.Value.FieldType, options);
            schemaField.Label = field.Value.Label;
            schemaField.SortKey = field.Value.SortKey;

            schema.Fields.Add(field.Key, schemaField);
        }

        return schema;
    }

    public static MongoContentSchema ToMongo(this ContentSchema schema)
    {
        MongoContentSchema mongoContentItem = new MongoContentSchema();

        mongoContentItem.Id = schema.Id;
        mongoContentItem.Name = schema.Name;
        mongoContentItem.CreatedAt = schema.CreatedAt;
        mongoContentItem.ModifiedAt = schema.ModifiedAt;
        mongoContentItem.Version = schema.Version;
        mongoContentItem.ListFields = schema.ListFields.ToList();
        mongoContentItem.ReferenceFields = schema.ReferenceFields.ToList();
        mongoContentItem.QueryFields = schema.QueryFields.ToList();
        mongoContentItem.OrderFields = schema.OrderFields.ToList();
        mongoContentItem.Preview = schema.Preview;

        foreach(var field in schema.Fields)
        {
            mongoContentItem.Fields.Add(field.Key, field.Value.ToMongo());
        }

        return mongoContentItem;
    }

    public static MongoSchemaField ToMongo(this SchemaField schemaField)
    {
        MongoSchemaField mongoSchemaField = new MongoSchemaField();

        mongoSchemaField.Label = schemaField.Label;
        mongoSchemaField.SortKey = schemaField.SortKey;
        mongoSchemaField.FieldType = schemaField.FieldType;

        if (schemaField.Options != null)
        {
            BsonDocument doc = schemaField.Options.ToBsonDocument(schemaField.Options.GetType());

            if (doc.ElementCount > 0)
            {
                mongoSchemaField.Options = doc;
            }
        }

        return mongoSchemaField;
    }
}
