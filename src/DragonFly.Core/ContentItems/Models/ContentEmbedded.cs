﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentEmbed
/// </summary>
public class ContentEmbedded : IContentElement
{
    public ContentEmbedded(ContentSchema schema)
    {
        _schema = schema;
        _fields = new ContentFields();
    }

    private ContentSchema _schema;

    /// <summary>
    /// Type
    /// </summary>
    public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

    private ContentFields _fields;

    /// <summary>
    /// Fields
    /// </summary>
    public virtual ContentFields Fields { get => _fields; set => _fields = value; }
  
}
