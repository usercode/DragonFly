// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly;

/// <summary>
/// ISchemaElement
/// </summary>
public interface ISchemaElement
{
    /// <summary>
    /// Fields
    /// </summary>
    SchemaFields Fields { get; }
}
