// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;

namespace DragonFly.API.Client;

/// <summary>
/// ContentService
/// </summary>
public partial class ClientContentService : IBackgroundTaskService
{
    public event Func<BackgroundTaskStatusChange, IBackgroundTaskInfo, Task> BackgroundTaskChanged;

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
}
