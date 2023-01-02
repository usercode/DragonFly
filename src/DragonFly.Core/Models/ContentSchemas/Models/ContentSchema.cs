// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// ContentSchema
/// </summary>
public class ContentSchema : ContentBase<ContentSchema>, ISchemaElement
{
    public ContentSchema(string name)
    {
        _name = name;
        _fields = new SchemaFields();
        _listFields = new List<string>();
        _referenceFields = new List<string>();
        _queryFields = new List<string>();
        _orderFields = new List<FieldOrder>();
    }

    private string _name;

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get => _name; set => _name = value; }

    private SchemaFields _fields;

    /// <summary>
    /// Fields
    /// </summary>
    public virtual SchemaFields Fields { get => _fields; set => _fields = value; }

    private IList<string> _listFields;

    /// <summary>
    /// ListFields
    /// </summary>
    public virtual IList<string> ListFields { get => _listFields; set => _listFields = value; }

    private IList<string> _referenceFields;

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public virtual IList<string> ReferenceFields { get => _referenceFields; set => _referenceFields = value; }

    private IList<FieldOrder> _orderFields;

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get => _orderFields; set => _orderFields = value; }

    private IList<string> _queryFields;

    public IList<string> QueryFields { get => _queryFields; set => _queryFields = value; }

    public override string ToString()
    {
        return Name;
    }
}
