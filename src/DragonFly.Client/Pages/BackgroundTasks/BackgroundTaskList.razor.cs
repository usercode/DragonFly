// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client.Pages.BackgroundTasks;

public partial class BackgroundTaskList : IAsyncDisposable
{
    [Inject]
    public IBackgroundTaskService BackgroundTaskService { get; set; }
       
    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public IToastService ToastService { get; set; }

    /// <summary>
    /// Tasks
    /// </summary>
    public Dictionary<int, BackgroundTaskInfo> Tasks { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.AddRefreshButton(this);
    }

    private IBackgroundTaskNotificationProvider Channel;

    private PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(800));

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Channel = (await BackgroundTaskService.StartNotificationProviderAsync()).Value;
        Channel.BackgroundTaskChanged +=
                                        async (state, task) =>
                                        {
                                            if (state == BackgroundTaskStatusChange.Removed)
                                            {
                                                Tasks.Remove(task.Id);
                                            }
                                            else
                                            {
                                                Tasks[task.Id] = task;
                                            }
                                        };

        Tasks = (await BackgroundTaskService.GetTasksAsync()).Value.ToDictionary(x => x.Id);

        RefreshByTimer();
    }

    private async void RefreshByTimer()
    {
        while (await _timer.WaitForNextTickAsync())
        {
            StateHasChanged();
        }
    }

    public async ValueTask DisposeAsync()
    {
        _timer.Dispose();

        if (Channel is not null)
        {
            await Channel.DisposeAsync();
        }
    }
}
