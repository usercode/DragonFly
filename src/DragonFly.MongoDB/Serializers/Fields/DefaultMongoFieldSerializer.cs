// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;
using MongoDB.Bson.Serialization;

namespace DragonFly.Storage.MongoDB.Fields;

/// <summary>
/// DefaultMongoFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public class DefaultMongoFieldSerializer<TContentField> : MongoFieldSerializer<TContentField>
    where TContentField : ContentField, new()
{
    public override TContentField Read(SchemaField schemaField, BsonValue bsonValue)
    {
        if (bsonValue is BsonDocument bsonDocument)
        {
            return BsonSerializer.Deserialize<TContentField>(bsonDocument);
        }

        return new TContentField();
    }

    public override BsonValue Write(TContentField contentField)
    {
        return contentField.ToBsonDocument(contentField.GetType());
    }
}
