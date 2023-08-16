// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

/// <summary>
/// MongoContentSchema
/// </summary>
public class MongoContentSchema : MongoContentBase
{
    public MongoContentSchema()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; }
    
    /// <summary>
    /// Parts
    /// </summary>
    public virtual IDictionary<string, MongoSchemaField> Fields { get; set; } = new Dictionary<string, MongoSchemaField>();

    /// <summary>
    /// ListFields
    /// </summary>
    public virtual IList<string> ListFields { get; set; } = new List<string>();

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public virtual IList<string> ReferenceFields { get; set; } = new List<string>();

    /// <summary>
    /// QueryFields
    /// </summary>
    public virtual IList<string> QueryFields { get; set; } = new List<string>();

    /// <summary>
    /// OrderFields
    /// </summary>
    public virtual IList<FieldOrder> OrderFields { get; set; } = new List<FieldOrder>();

    /// <summary>
    /// Preview
    /// </summary>
    public virtual PreviewItem Preview { get; set; } = new PreviewItem();

    public override string ToString()
    {
        return Name;
    }
}
