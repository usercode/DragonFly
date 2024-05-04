// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.API.Client.BackgroundTasks;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IBackgroundTaskService
{
    public async Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        var response = await Client.PostAsync("api/task/query", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        BackgroundTaskInfo[]? result = await response.Content.ReadFromJsonAsync<BackgroundTaskInfo[]>();

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }

    public async Task<Result> CancelAsync(int id)
    {
        var response = await Client.PostAsync($"api/task/{id}/cancel", new StringContent(string.Empty));

        return await response.ToResultAsync();
    }

    public async Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        SignalRBackgroundTaskNotificationProvider provider = new SignalRBackgroundTaskNotificationProvider(NavigationManager);

        await provider.StartAsync();

        return provider;
    }
}
