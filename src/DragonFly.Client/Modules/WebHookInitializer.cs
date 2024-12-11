// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;
using MudBlazor;

namespace DragonFly.Client;

/// <summary>
/// WebHookModule
/// </summary>
public class WebHookInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu().Add("Webhook", Icons.Material.Filled.SignalWifi4Bar, "webhook");

        return Task.CompletedTask;
    }
}
