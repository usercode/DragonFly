// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Client.Pages.SystemTools;
using DragonFly.Init;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// SettingsModule
/// </summary>
public class SettingsInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {        
        api.MainMenu().Add("Settings", new Icons.Filled.Size24.Settings(), "settings");

        SettingsManager.Default.Add<SystemTools>("Tools");

        return Task.CompletedTask;
    }
}
