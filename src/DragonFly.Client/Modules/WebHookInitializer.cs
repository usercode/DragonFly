// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// WebHookModule
/// </summary>
public class WebHookInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu().Add("Webhook", new Icons.Filled.Size24.DesktopSignal(), "webhook");

        return Task.CompletedTask;
    }
}
