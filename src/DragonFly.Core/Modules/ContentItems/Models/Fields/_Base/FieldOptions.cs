// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldOptions
/// </summary>
public abstract class FieldOptions
{
    /// <summary>
    /// IsRequired
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// HasIndex
    /// </summary>
    public bool HasIndex { get; set; }

    /// <summary>
    /// CreateContentField
    /// </summary>
    public abstract ContentField CreateContentField();
}
