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
            ContentSchema schema = new ContentSchema();

            schema.Id = mongoSchema.Id;
            schema.Name = mongoSchema.Name;
            schema.CreatedAt = mongoSchema.CreatedAt;
            schema.ModifiedAt = mongoSchema.ModifiedAt;
            schema.Version = mongoSchema.Version;
            schema.ListFields = mongoSchema.ListFields.ToList();
            schema.ReferenceFields = mongoSchema.ReferenceFields.ToList();
            schema.OrderFields = mongoSchema.OrderFields.ToList();

            foreach(var mongoField in mongoSchema.Fields)
            {
                ContentFieldDefinition definition = new ContentFieldDefinition();

                definition.Label = mongoField.Value.Label;
                definition.SortKey = mongoField.Value.SortKey;
                definition.FieldType = mongoField.Value.FieldType;
                
                ContentFieldOptions options = ContentFieldManager.Default.CreateField(mongoField.Value.FieldType).CreateOptions();

                if (options != null && mongoField.Value.Options != BsonNull.Value)
                {
                    definition.Options = (ContentFieldOptions)BsonSerializer.Deserialize((BsonDocument)mongoField.Value.Options, options.GetType());
                }

                if(definition.Options == null)
                {
                    definition.Options = options;
                }

                schema.Fields.Add(mongoField.Key, definition);
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

                    if(doc.ElementCount > 0)
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
