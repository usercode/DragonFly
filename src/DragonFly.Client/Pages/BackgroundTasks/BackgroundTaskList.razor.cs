// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Client.Base;
using DragonFly.Core;
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
    public IDictionary<int, BackgroundTaskInfo> Tasks { get; set; }

    private HubConnection _hubConnection;

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.AddRefreshButton(this);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _hubConnection = new HubConnectionBuilder().WithUrl(Navigation.ToAbsoluteUri("/dragonfly/taskhub")).Build();
        _hubConnection.On<BackgroundTaskChange, BackgroundTaskInfo>("TaskChanged", async (state, task) =>
        {
            await InvokeAsync(async () =>
            {
                if (state == BackgroundTaskChange.Removed)
                {
                    Tasks.Remove(task.Id);
                }
                else
                {
                    Tasks[task.Id] = task;
                }

                StateHasChanged();
            });
        });

        await _hubConnection.StartAsync();
    }

    protected override async Task RefreshActionAsync()
    {
        Tasks = (await BackgroundTaskService.GetTasksAsync()).ToDictionary(x => x.Id);
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
