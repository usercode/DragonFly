// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentPartDefinition
/// </summary>
public class MongoSchemaField
{
    /// <summary>
    /// Label
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// SortKey
    /// </summary>
    public int SortKey { get; set; }

    /// <summary>
    /// FieldType
    /// </summary>
    public string FieldType { get; set; } = string.Empty;

    /// <summary>
    /// Options
    /// </summary>
    public BsonValue Options {get;set;} = BsonNull.Value;
}
