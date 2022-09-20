using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Contents.Content;
using MongoDB.Bson;

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
