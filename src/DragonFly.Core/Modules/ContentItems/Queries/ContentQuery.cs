﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core;

namespace DragonFly.Query;

/// <summary>
/// ContentQuery
/// </summary>
public class ContentQuery : QueryBase
{
    public ContentQuery(string schema)
        : this()
    {
        Schema = schema;
    }

    public ContentQuery()
    {
        Fields = new List<FieldQuery>();
        OrderFields = new List<FieldOrder>();

        Top = 25;
        Published = true;

        IncludeListFieldsOnly = false;
    }

    /// <summary>
    /// Schema
    /// </summary>
    public string? Schema { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public IList<FieldQuery> Fields { get; set; }

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IncludeListFieldsOnly { get; set; }

    /// <summary>
    /// UsedAsset
    /// </summary>
    public Guid? UsedAsset { get; set; }

    /// <summary>
    /// Published
    /// </summary>
    public bool Published { get; set; }
}