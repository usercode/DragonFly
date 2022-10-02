// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Query;

/// <summary>
/// FieldQuery
/// </summary>
public abstract class FieldQuery
{
    /// <summary>
    /// Type
    /// </summary>
    public virtual string Type => GetType().Name;

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
