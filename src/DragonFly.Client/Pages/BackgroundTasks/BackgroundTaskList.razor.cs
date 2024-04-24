// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Results;

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

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.AddRefreshButton(this);
    }

    private IBackgroundTaskNotificationProvider Channel;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Channel = (await BackgroundTaskService.StartNotificationProviderAsync()).Value;
        Channel.BackgroundTaskChanged +=
                                        async (state, task) =>
                                        {
                                            await InvokeAsync(() =>
                                            {
                                                if (state == BackgroundTaskStatusChange.Removed)
                                                {
                                                    Tasks.Remove(task.Id);
                                                }
                                                else
                                                {
                                                    Tasks[task.Id] = task;
                                                }

                                                StateHasChanged();

                                                return Task.CompletedTask;
                                            });
                                        };
    }

    protected override async Task RefreshActionAsync()
    {
        Tasks = (await BackgroundTaskService.GetTasksAsync()).Value.ToDictionary(x => x.Id);
    }

    public async ValueTask DisposeAsync()
    {
        if (Channel is not null)
        {
            await Channel.DisposeAsync();
        }
    }
}
