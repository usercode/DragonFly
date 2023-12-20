// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.Storage.Abstractions;

/// <summary>
/// IMongoFieldSerializer
/// </summary>
public interface IMongoFieldSerializer
{
    /// <summary>
    /// FieldType
    /// </summary>
    Type FieldType { get; }

    /// <summary>
    /// Read
    /// </summary>
    ContentField Read(SchemaField schemaField, BsonValue bsonValue);

    /// <summary>
    /// Write
    /// </summary>
    BsonValue Write(ContentField contentField);
}
