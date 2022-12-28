// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Client;

/// <summary>
/// SettingsItem
/// </summary>
public class SettingsItem
{
    public SettingsItem(string name, Type componentType)
    {
        Name = name;
        ComponentType = componentType;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// ComponentType
    /// </summary>
    public Type ComponentType { get; }

}
