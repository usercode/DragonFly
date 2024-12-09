// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ArrayFieldOptions
/// </summary>
public class ArrayFieldOptions : FieldOptions, ISchemaElement
{
    /// <summary>
    /// Fields
    /// </summary>
    public SchemaFields Fields { get; set; } = [];

    /// <summary>
    /// MinItems
    /// </summary>
    public int? MinItems { get; set; }

    /// <summary>
    /// MaxItems
    /// </summary>
    public int? MaxItems { get; set; }

    public override ContentField CreateContentField()
    {
        return new ArrayField();
    }
}
