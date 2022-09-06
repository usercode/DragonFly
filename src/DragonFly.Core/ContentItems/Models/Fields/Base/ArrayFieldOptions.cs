using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ArrayFieldOptions
/// </summary>
public class ArrayFieldOptions : ContentFieldOptions, ISchemaElement
{
    public ArrayFieldOptions()
    {
        Fields = new SchemaFields();
    }

    /// <summary>
    /// Fields
    /// </summary>
    public SchemaFields Fields { get; set; }

    /// <summary>
    /// MinItems
    /// </summary>
    public int? MinItems { get; set; }

    /// <summary>
    /// MaxItems
    /// </summary>
    public int? MaxItems { get; set; }

    public override IContentField CreateContentField()
    {
        return new ArrayField();
    }
}
