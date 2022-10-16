// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoAsset
/// </summary>
public class MongoAsset : MongoContentBase
{
    public MongoAsset()
    {
        Metaddata = new Dictionary<string, BsonDocument>();
    }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Alt
    /// </summary>
    public string? Alt { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Filename
    /// </summary>
    public string? Slug { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Hash
    /// </summary>
    public string? Hash { get; set; }

    /// <summary>
    /// ContentType
    /// </summary>
    public string? MimeType { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public Guid? Folder { get; set; }

    /// <summary>
    /// Metaddata
    /// </summary>
    public IDictionary<string, BsonDocument> Metaddata { get; set; }
}
