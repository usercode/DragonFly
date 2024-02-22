﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// SettingsModule
/// </summary>
public class SettingsInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {        
        api.MainMenu().Add("Settings", "fa-solid fa-gear", "settings");

        return Task.CompletedTask;
    }
}
