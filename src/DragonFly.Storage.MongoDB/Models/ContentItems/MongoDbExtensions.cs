using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using DragonFly.Data.Content;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;
using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Data.Models
{
    public static class MongoDbExtensions
    {
        public static void ToModelValue(this BsonValue bsonValue, string fieldName, IContentElement contentItem, ISchemaElement schema)
        {
            if (bsonValue == BsonNull.Value)
            {
                return;
            }

            if (schema.Fields.TryGetValue(fieldName, out SchemaField? schemaField))
            {
                IFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByFieldType(ContentFieldManager.Default.GetContentFieldType(schemaField.FieldType));

                ContentField contentField = fieldSerializer.Read(schemaField, bsonValue);

                contentItem.SetField(fieldName, contentField);
            }
        }

        public static BsonValue ToBsonValue(this ContentField contentField)
        {
            IFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByFieldType(contentField.GetType());

            return fieldSerializer.Write(contentField);
        }
    }
}
