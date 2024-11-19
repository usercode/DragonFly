// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.SignalR.Client;

namespace DragonFly.API.Client.BackgroundTasks;

public sealed class SignalRBackgroundTaskNotificationProvider : IBackgroundTaskNotificationProvider
{
    public SignalRBackgroundTaskNotificationProvider(RestApiClient client)
    {
        Client = client;
    }

    /// <summary>
    /// NavigationManager
    /// </summary>
    private RestApiClient Client { get; }

    /// <summary>
    /// HubConnection
    /// </summary>
    private HubConnection? HubConnection { get; set; }

    /// <summary>
    /// BackgroundTaskChanged
    /// </summary>
    public event BackgroundTaskHandler? BackgroundTaskChanged;

    /// <summary>
    /// StartAsync
    /// </summary>
    public async Task StartAsync()
    {
        HubConnection = new HubConnectionBuilder().WithUrl(new Uri(Client.HttpClient.BaseAddress, "/dragonfly/background-task-hub")).Build();
        HubConnection.On<BackgroundTaskStatusChange, BackgroundTaskInfo>("TaskChanged", async (state, task) =>
        {
            if (BackgroundTaskChanged != null)
            {
                await BackgroundTaskChanged.Invoke(state, task);
            }
        });

        await HubConnection.StartAsync();
    }

    /// <summary>
    /// DisposeAsync
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (HubConnection is not null)
        {
            await HubConnection.DisposeAsync();
        }
    }
}
