// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// BackgroundTaskModule
/// </summary>
public class BackgroundTaskInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {
        api.MainMenu().Add("Tasks", "fa-solid fa-layer-group", "tasks");

        return Task.CompletedTask;
    }
}
