// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.Storage.Abstractions;

/// <summary>
/// IFieldSerializer
/// </summary>
public interface IFieldSerializer
{
    /// <summary>
    /// FieldType
    /// </summary>
    Type FieldType { get; }

    /// <summary>
    /// Read
    /// </summary>
    /// <param name="bsonValue"></param>
    /// <returns></returns>
    ContentField Read(SchemaField schemaField, BsonValue bsonValue);

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="contentField"></param>
    /// <returns></returns>
    BsonValue Write(ContentField contentField);
}
