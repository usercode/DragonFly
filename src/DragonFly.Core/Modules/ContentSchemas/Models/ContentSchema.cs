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

    private SchemaFields _fields = new SchemaFields();

    /// <summary>
    /// Fields
    /// </summary>
    public virtual SchemaFields Fields { get => _fields; set => _fields = value; }

    private IList<string> _listFields = new List<string>();

    /// <summary>
    /// ListFields
    /// </summary>
    public virtual IList<string> ListFields { get => _listFields; set => _listFields = value; }

    private IList<string> _referenceFields = new List<string>();

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public virtual IList<string> ReferenceFields { get => _referenceFields; set => _referenceFields = value; }

    private IList<FieldOrder> _orderFields = new List<FieldOrder>();

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get => _orderFields; set => _orderFields = value; }

    private IList<string> _queryFields = new List<string>();

    /// <summary>
    /// QueryFields
    /// </summary>
    public IList<string> QueryFields { get => _queryFields; set => _queryFields = value; }

    private PreviewItem _previewOptions = new PreviewItem();

    /// <summary>
    /// Preview
    /// </summary>
    public PreviewItem Preview { get => _previewOptions; set => _previewOptions = value; }

    public override bool Equals(object? obj)
    {
        if (obj is ContentSchema other)
        {
            return Name == other.Name;
        }

        return false;
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
