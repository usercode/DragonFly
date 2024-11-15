// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoReferencedContent
/// </summary>
public class MongoReferencedContent
{
    public MongoReferencedContent()
    {
        
    }

    public MongoReferencedContent(string schema, Guid id)
    {
        Schema = schema;
        Id2 = id;
    }

    /// <summary>
    /// Schema
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id2 { get; set; }

    public override string ToString()
    {
        return $"{Schema}/{Id2}";
    }
}
