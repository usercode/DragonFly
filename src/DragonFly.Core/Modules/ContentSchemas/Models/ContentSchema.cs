// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentSchema
/// </summary>
public class ContentSchema : ContentBase<ContentSchema>, ISchemaElement
{
    public ContentSchema(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        _name = name;
    }

    private string _name;

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get => _name; set => _name = value; }

    /// <summary>
    /// Fields
    /// </summary>
    public virtual SchemaFields Fields { get; set; } = new SchemaFields();

    /// <summary>
    /// ListFields
    /// </summary>
    public virtual IList<string> ListFields { get; set; } = new List<string>();

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public virtual IList<string> ReferenceFields { get; set; } = new List<string>();

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; } = new List<FieldOrder>();

    /// <summary>
    /// QueryFields
    /// </summary>
    public IList<string> QueryFields { get; set; } = new List<string>();

    /// <summary>
    /// Preview
    /// </summary>
    public PreviewItem Preview { get; set; } = new PreviewItem();

    public override bool Equals(ContentSchema? obj)
    {
        if (obj is null)
        {
            return false;
        }

        return Name == obj.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(ContentSchema), Name);
    }

    public override string ToString()
    {
        return Name;
    }
}
