// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;
using Microsoft.FluentUI.AspNetCore.Components;
using static Microsoft.FluentUI.AspNetCore.Components.Icons.Regular.Size24;

namespace DragonFly.Client;

/// <summary>
/// WebHookModule
/// </summary>
public class WebHookInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu().Add("Webhook", new DesktopSignal(), "webhook");

        return Task.CompletedTask;
    }
}
