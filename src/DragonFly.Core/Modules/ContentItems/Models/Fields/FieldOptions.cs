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
    /// IsSearchable
    /// </summary>
    public bool IsSearchable { get; set; }

    public abstract ContentField CreateContentField();
}
