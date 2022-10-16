// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoEvent
/// </summary>
public class MongoEvent : MongoContentBase
{
    public MongoEvent()
    {
        Name = string.Empty;
        Data = BsonNull.Value;
    }

    /// <summary>
    /// Date
    /// </summary>
    public virtual DateTime? Date { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public BsonValue Data { get; set; }
}
