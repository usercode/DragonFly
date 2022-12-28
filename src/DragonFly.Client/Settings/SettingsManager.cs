// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace DragonFly.Client;

public class SettingsManager
{
    private static SettingsManager _default;

    public static SettingsManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new SettingsManager();
            }

            return _default;
        }
    }

    public SettingsManager()
    {
        Items = new List<SettingsItem>();
    }

    public IList<SettingsItem> Items { get; }

    public void Add<T>(string title)
        where T : ComponentBase
    {
        Add(title, typeof(T));
    }

    public void Add(string title, Type componentType)
    {
        Items.Add(new SettingsItem(title, componentType));
    }
}
