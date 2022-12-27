// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;

namespace DragonFly.API.Json;

/// <summary>
/// IJsonFieldSerializer
/// </summary>
public interface IJsonFieldSerializer
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
    ContentField Read(SchemaField schemaField, JsonNode? bsonValue);

    /// <summary>
    /// Write
    /// </summary>
    /// <param name="contentField"></param>
    /// <returns></returns>
    JsonNode? Write(ContentField contentField, bool includeNavigationProperty);
}
