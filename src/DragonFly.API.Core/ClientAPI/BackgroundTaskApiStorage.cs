// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API.Client.BackgroundTasks;
using SmartResults;

namespace DragonFly.API.Client;

/// <summary>
/// BackgroundTaskStorage
/// </summary>
internal class BackgroundTaskApiStorage : IBackgroundTaskService
{
    public BackgroundTaskApiStorage(RestApiClient restClient)
    {
        RestClient = restClient;
        Client = restClient.HttpClient;
    }

    private HttpClient Client { get; }

    private RestApiClient RestClient { get; }

    public async Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        return await Client
                        .PostAsync("api/task/query", new StringContent(string.Empty))
                        .ReadResultFromJsonAsync<BackgroundTaskInfo[]>(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result> CancelAsync(int id)
    {
        return await Client
                        .PostAsync($"api/task/{id}/cancel", new StringContent(string.Empty))
                        .ReadResultFromJsonAsync(ApiJsonSerializerDefault.Options);
    }

    public async Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        SignalRBackgroundTaskNotificationProvider provider = new SignalRBackgroundTaskNotificationProvider(RestClient);

        await provider.StartAsync();

        return provider;
    }
}
