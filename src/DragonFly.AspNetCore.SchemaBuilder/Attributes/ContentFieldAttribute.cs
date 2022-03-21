using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes;

/// <summary>
/// ContentFieldAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ContentFieldAttribute : Attribute
{
    public ContentFieldAttribute()
    {

    }

    public ContentFieldAttribute(string name, Type fieldType)
    {
        Name = name;
        FieldType = fieldType;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// FieldType
    /// </summary>
    public Type? FieldType { get; set; }
}
