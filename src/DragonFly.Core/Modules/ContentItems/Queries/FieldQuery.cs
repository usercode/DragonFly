// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// FieldQuery
/// </summary>
public abstract class FieldQuery
{
    /// <summary>
    /// FieldName
    /// </summary>
    public virtual string? FieldName { get; set; }

    /// <summary>
    /// IsEmpty
    /// </summary>
    /// <returns></returns>
    public virtual bool IsEmpty() => false;
}
