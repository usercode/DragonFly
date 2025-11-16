// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// WebHookModule
/// </summary>
public class WebHookInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu.Add("Webhook", "fa-solid fa-satellite-dish", "webhook");

        return Task.CompletedTask;
    }
}
