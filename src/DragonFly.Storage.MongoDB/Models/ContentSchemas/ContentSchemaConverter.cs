using DragonFly.Content;
using DragonFly.ContentTypes;
using DragonFly.Data.Content;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Data.Models
{
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
            schema.OrderFields = mongoSchema.OrderFields.ToList();

            foreach(var field in mongoSchema.Fields)
            {
                try
                {
                    Type? optionsType = ContentFieldManager.Default.GetOptionsType(field.Value.FieldType);
                    ContentFieldOptions? options = null;

                    if (field.Value.Options != BsonNull.Value)
                    {
                        options = (ContentFieldOptions)BsonSerializer.Deserialize((BsonDocument)field.Value.Options, optionsType);
                    }

                    if (options == null)
                    {
                        options = ContentFieldManager.Default.CreateOptions(field.Value.FieldType);
                    }

                    SchemaField schemaField = new SchemaField(field.Value.FieldType, options);
                    schemaField.Label = field.Value.Label;
                    schemaField.SortKey = field.Value.SortKey;

                    schema.Fields.Add(field.Key, schemaField);
                }
                catch(Exception ex)
                {
                    throw;
                }
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
            mongoContentItem.OrderFields = schema.OrderFields.ToList();

            foreach(var field in schema.Fields)
            {
                MongoContentFieldDefinition definition = new MongoContentFieldDefinition();

                definition.Label = field.Value.Label;
                definition.SortKey = field.Value.SortKey;
                definition.FieldType = field.Value.FieldType;

                if (field.Value.Options != null)
                {
                    BsonDocument doc = field.Value.Options.ToBsonDocument(field.Value.Options.GetType());

                    if (doc.ElementCount > 0)
                    {
                        definition.Options = doc;
                    }
                }

                mongoContentItem.Fields.Add(field.Key, definition);
            }

            return mongoContentItem;
        }
    }
}
