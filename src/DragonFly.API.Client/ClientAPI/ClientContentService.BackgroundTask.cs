// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.API.Client.BackgroundTasks;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IBackgroundTaskService
{
    public async Task<IBackgroundTaskInfo[]> GetTasksAsync()
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

    public async Task CancelAsync(int id)
    {
        var response = await Client.PostAsync($"api/task/{id}/cancel", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }

    public async Task<IBackgroundTaskNotificationProvider> StartNotificationProviderAsync()
    {
        SignalRBackgroundTaskNotificationProvider provider = new SignalRBackgroundTaskNotificationProvider(NavigationManager);

        await provider.StartAsync();

        return provider;
    }
}
