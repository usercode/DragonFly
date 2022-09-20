using System;

namespace DragonFly.Proxy.Attributes;

/// <summary>
/// ContentItemAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ContentItemAttribute : Attribute
{
    public ContentItemAttribute(string? schemaName = null)
    {
        SchemaName = schemaName;
    }

    /// <summary>
    /// SchemaName
    /// </summary>
    public string? SchemaName { get; set; }
}
