// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly;

/// <summary>
/// ReferenceField
/// </summary>
[FieldOptions(typeof(ReferenceFieldOptions))]
[FieldQuery(typeof(ReferenceFieldQuery))]
public class ReferenceField : ContentField
{
    public const string IdField = "Id";
    public const string SchemaField = "Schema";

    public ReferenceField()
    {
    }

    public ReferenceField(ContentItem? contentItem)
    {
        ContentItem = contentItem;
    }

    /// <summary>
    /// ContentItem
    /// </summary>
    public ContentItem? ContentItem { get; set; }

    public override string ToString()
    {
        if (ContentItem == null)
        {
            return "no reference";
        }

        return ContentItem.ToString();
    }
}
