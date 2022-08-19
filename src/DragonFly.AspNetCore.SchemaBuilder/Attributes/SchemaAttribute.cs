using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

/// <summary>
/// SchemaAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SchemaAttribute : Attribute
{
    public SchemaAttribute()
    {

    }

    public SchemaAttribute(string schemaName)
    {
        SchemaName = schemaName;
    }

    /// <summary>
    /// SchemaName
    /// </summary>
    public string? SchemaName { get; set; }
}
