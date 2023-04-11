// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using DragonFly.Query;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IBackgroundTaskService
{
    public async Task<IEnumerable<BackgroundTaskInfo>> GetTasksAsync()
    {
        var response = await Client.PostAsync("api/task/query", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();

        IEnumerable<BackgroundTaskInfo>? result = await response.Content.ReadFromJsonAsync<IEnumerable<BackgroundTaskInfo>>();

        return result;
    }

    public async Task CancelAsync(int id)
    {
        var response = await Client.PostAsync($"api/task/{id}/cancel", new StringContent(string.Empty));

        response.EnsureSuccessStatusCode();
    }
}
