// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// FieldQueryAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FieldQueryAttribute : Attribute
{
    public FieldQueryAttribute(Type queryType)
    {
        QueryType = queryType;
    }

    /// <summary>
    /// QueryType
    /// </summary>
    public Type QueryType { get; }
}
