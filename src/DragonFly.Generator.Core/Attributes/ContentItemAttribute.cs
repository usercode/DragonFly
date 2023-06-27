// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class ContentItemAttribute : Attribute
{
    public ContentItemAttribute()
    {
            
    }

    public ContentItemAttribute(string schemaName)
    {
        SchemaName = schemaName;
    }

    /// <summary>
    /// SchemaName
    /// </summary>
    public string? SchemaName { get; set; }
}
