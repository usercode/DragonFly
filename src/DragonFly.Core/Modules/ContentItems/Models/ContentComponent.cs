﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentComponent
/// </summary>
public class ContentComponent : IContentElement
{
    public ContentComponent(ContentSchema schema)
    {
        _schema = schema;
    }

    private ContentSchema _schema;

    /// <summary>
    /// Type
    /// </summary>
    public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

    private ContentFields _fields = new ContentFields();

    /// <summary>
    /// Fields
    /// </summary>
    public virtual ContentFields Fields { get => _fields; set => _fields = value; }
  
}
