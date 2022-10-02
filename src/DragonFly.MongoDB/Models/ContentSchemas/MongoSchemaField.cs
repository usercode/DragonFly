﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentPartDefinition
/// </summary>
public class MongoSchemaField
{
    public MongoSchemaField()
    {
        FieldType = string.Empty;
        Options = BsonNull.Value;
    }

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
    public string FieldType { get; set; }

    /// <summary>
    /// Options
    /// </summary>
    public BsonValue Options {get;set;}
}
