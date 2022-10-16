// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// ContentItem
/// </summary>
public class MongoContentItem : MongoContentBase
{
    public MongoContentItem()
    {
        Fields = new MongoContentFields();
    }

    /// <summary>
    /// SchemaVersion
    /// </summary>
    public int SchemaVersion { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public MongoContentFields Fields { get; set; }
}
