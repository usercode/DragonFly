// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.Storage.Abstractions;

/// <summary>
/// MongoFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public abstract class MongoFieldSerializer<TContentField> : IMongoFieldSerializer
    where TContentField : ContentField
{
    public Type FieldType => typeof(TContentField);

    public abstract TContentField Read(SchemaField schemaField, BsonValue bsonValue);

    public abstract BsonValue Write(TContentField contentField);

    public BsonValue Write(ContentField contentField)
    {
        return Write((TContentField)contentField);
    }

    ContentField IMongoFieldSerializer.Read(SchemaField definition, BsonValue bsonvalue)
    {
        return Read(definition, bsonvalue);
    }
}
