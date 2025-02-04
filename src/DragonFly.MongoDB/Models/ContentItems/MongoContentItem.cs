﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// ContentItem
/// </summary>
public class MongoContentItem : MongoContentBase
{
    /// <summary>
    /// SchemaVersion
    /// </summary>
    public int SchemaVersion { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public MongoContentFields Fields { get; set; } = [];

    /// <summary>
    /// References to other content items.
    /// </summary>
    public IList<MongoReferencedContent> ReferencedTo { get; set; } = [];
}
