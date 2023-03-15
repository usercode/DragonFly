// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DragonFly.Client.Pages.BackgroundTasks;

public class BackgroundTaskListBase : StartComponentBase, IAsyncDisposable
{
    [Inject]
    public IBackgroundTaskService BackgroundTaskService { get; set; }
       
    [Inject]
    public NavigationManager Navigation { get; set; }

    /// <summary>
    /// Tasks
    /// </summary>
    public IEnumerable<BackgroundTaskInfo> Tasks { get; set; }

    private HubConnection _hubConnection;

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.AddRefreshButton(this);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _hubConnection = new HubConnectionBuilder().WithUrl(Navigation.ToAbsoluteUri("/dragonfly/taskhub")).Build();
        _hubConnection.On("TaskChanged", async () =>
        {
            await InvokeAsync(async () =>
            {
                await RefreshAsync();
            });
        });

        await _hubConnection.StartAsync();
    }

    protected override async Task RefreshActionAsync()
    {
        Tasks = await BackgroundTaskService.GetTasksAsync();
    }

    protected async Task CancelTask(BackgroundTaskInfo task)
    {
        await BackgroundTaskService.CancelAsync(task.Id);

        await RefreshAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
