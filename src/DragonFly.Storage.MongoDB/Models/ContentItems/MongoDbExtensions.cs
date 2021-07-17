using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using DragonFly.Data.Content;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;
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
        public static void FromBsonValue(this BsonValue bsonValue, string fieldName, IContentElement contentItem, ISchemaElement schema)
        {
            if (bsonValue == BsonNull.Value)
            {
                return;
            }

            if (contentItem.Fields.TryGetValue(fieldName, out ContentField? contentField))
            {
                if (schema.Fields.TryGetValue(fieldName, out ContentSchemaField? schemaField))
                {
                    IFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByType(contentField.GetType());

                    fieldSerializer.Read(schemaField, contentField, bsonValue);
                }
            }
        }

        public static BsonValue ToBsonValue(this ContentField contentField)
        {
            IFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByType(contentField.GetType());

            return fieldSerializer.Write(contentField);
        }
    }
}
