using DragonFly.Storage.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.MongoDB;

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

            IContentField contentField = fieldSerializer.Read(schemaField, bsonValue);

            contentItem.TrySetField(fieldName, contentField);
        }
    }

    public static BsonValue ToBsonValue(this IContentField contentField)
    {
        IFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByFieldType(contentField.GetType());

        return fieldSerializer.Write(contentField);
    }
}
