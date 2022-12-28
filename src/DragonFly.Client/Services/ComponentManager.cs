// Copyright (c) usercode
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
    private static ComponentManager _default;

    public static ComponentManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new ComponentManager();
            }

            return _default;
        }
    }

    private IDictionary<Type, Type> _cacheFieldView;

    private ComponentManager()
    {
        _cacheFieldView = new Dictionary<Type, Type>();
    }

    public void Register(Type fieldType, Type componentType)
    {
        _cacheFieldView[fieldType] = componentType;
    }

    public Type GetComponentType(Type fieldType)
    {
        if (_cacheFieldView.TryGetValue(fieldType, out Type componentType))
        {
            return componentType;
        }

        return null;
    }
}
