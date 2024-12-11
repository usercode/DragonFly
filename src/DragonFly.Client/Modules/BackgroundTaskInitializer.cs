// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Init;
using MudBlazor;

namespace DragonFly.Client;

/// <summary>
/// BackgroundTaskModule
/// </summary>
public class BackgroundTaskInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {
        api.MainMenu().Add("Tasks", Icons.Material.Filled.Task, "tasks");

        return Task.CompletedTask;
    }
}
