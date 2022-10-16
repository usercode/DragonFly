// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;
using MongoDB.Bson.Serialization;

namespace DragonFly.Storage.MongoDB.Fields;

public class DefaultFieldSerializer<TContentField> : FieldSerializer<TContentField>
    where TContentField : IContentField, new()
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
