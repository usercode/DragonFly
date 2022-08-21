using System;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

/// <summary>
/// SchemaAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SchemaAttribute : Attribute
{
    public SchemaAttribute(string? schemaName = null)
    {
        SchemaName = schemaName;
    }

    /// <summary>
    /// SchemaName
    /// </summary>
    public string? SchemaName { get; set; }
}
