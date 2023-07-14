// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Storage.Abstractions;
using MongoDB.Bson;

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
            IMongoFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByFieldType(FieldManager.Default.GetFieldType(schemaField.FieldType));

            ContentField contentField = fieldSerializer.Read(schemaField, bsonValue);

            contentItem.SetField(fieldName, contentField);
        }
    }

    public static BsonValue ToBsonValue(this ContentField contentField)
    {
        IMongoFieldSerializer fieldSerializer = MongoFieldManager.Default.GetByFieldType(contentField.GetType());

        return fieldSerializer.Write(contentField);
    }
}
