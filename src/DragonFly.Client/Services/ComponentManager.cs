﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;

namespace DragonFly.Client;

/// <summary>
/// ComponentManager
/// </summary>
public sealed class ComponentManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static ComponentManager Default { get; } = new ComponentManager();

    private IDictionary<Type, Type> _cacheFieldView;

    private ComponentManager()
    {
        _cacheFieldView = new Dictionary<Type, Type>();
    }

    public void Add(Type fieldType, Type componentType)
    {
        _cacheFieldView[fieldType] = componentType;
    }

    public Type GetComponentType(Type type)
    {
        if (_cacheFieldView.TryGetValue(type, out Type componentType))
        {
            return componentType;
        }

        return null;
    }

    public Type GetComponentType<T>()
    {
        return GetComponentType(typeof(T));
    }
}
