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

    public ContentItemAttribute(string schema)
    {
        Schema = schema;
    }

    /// <summary>
    /// SchemaName
    /// </summary>
    public string? Schema { get; set; }

    /// <summary>
    /// Pattern
    /// </summary>
    public string? Preview { get; set; }

    public void ApplyToSchema(ContentSchema schema)
    {
        if (Schema != null)
        {
            schema.Name = Schema;
        }

        if (Preview != null)
        {
            schema.Preview.Pattern = Preview;
        }
    }
}
