// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly;

/// <summary>
/// ComponentAdded
/// </summary>
public sealed class ComponentAdded
{
    public ComponentAdded(Type fieldType)
    {
        FieldType = fieldType;
    }

    /// <summary>
    /// FieldType
    /// </summary>
    public Type FieldType { get; }
}
