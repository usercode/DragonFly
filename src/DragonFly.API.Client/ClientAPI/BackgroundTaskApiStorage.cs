// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API.Client.BackgroundTasks;
using Microsoft.AspNetCore.Components;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// BackgroundTaskStorage
/// </summary>
internal class BackgroundTaskApiStorage : IBackgroundTaskService
{
    public BackgroundTaskApiStorage(HttpClient client, NavigationManager navigationManager)
    {
        Client = client; 
        NavigationManager = navigationManager;
    }

    private HttpClient Client { get; }

    private NavigationManager NavigationManager { get; }

    public async Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        return await Client
                        .PostAsync("api/task/query", new StringContent(string.Empty))
                        .ToResultAsync<BackgroundTaskInfo[]>();
    }

    public async Task<Result> CancelAsync(int id)
    {
        return await Client
                        .PostAsync($"api/task/{id}/cancel", new StringContent(string.Empty))
                        .ToResultAsync();
    }

    public async Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        SignalRBackgroundTaskNotificationProvider provider = new SignalRBackgroundTaskNotificationProvider(NavigationManager);

        await provider.StartAsync();

        return provider;
    }
}
