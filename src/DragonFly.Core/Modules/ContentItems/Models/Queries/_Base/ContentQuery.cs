// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentQuery
/// </summary>
public class ContentQuery : QueryBase
{
    public ContentQuery(string schema)
        : this()
    {
        Schema = schema;
    }

    public ContentQuery()
    {
    }

    /// <summary>
    /// Schema
    /// </summary>
    public string Schema { get; set; } = string.Empty;

    /// <summary>
    /// Fields
    /// </summary>
    public IList<FieldQuery> Fields { get; set; } = [];

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; } = [];

    /// <summary>
    /// IncludeListFieldsOnly
    /// </summary>
    public bool IncludeListFieldsOnly { get; set; } = false;

    /// <summary>
    /// Reference
    /// </summary>
    public ContentReference? Reference { get; set; } = null;

    /// <summary>
    /// Published
    /// </summary>
    public bool Published { get; set; } = true;
}
