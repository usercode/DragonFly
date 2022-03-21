using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonFly.Content.Queries;

/// <summary>
/// ContentItemQuery
/// </summary>
public class ContentItemQuery
{
    public ContentItemQuery()
    {
        Fields = new List<FieldQuery>();
        OrderFields = new List<FieldOrder>();

        SearchPattern = string.Empty;
        Skip = 0;
        Top = 20;

        IncludeListFieldsOnly = false;
    }

    /// <summary>
    /// Schema
    /// </summary>
    public string? Schema { get; set; }

    /// <summary>
    /// Skip
    /// </summary>
    public int Skip { get; set; }

    /// <summary>
    /// Top
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// SearchPattern
    /// </summary>
    public string SearchPattern { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    public IList<FieldQuery> Fields { get; set; }

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IncludeListFieldsOnly { get; set; }

}
