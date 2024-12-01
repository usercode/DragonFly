// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client.Pages.BackgroundTasks;

public partial class BackgroundTaskComponent
{
    /// <summary>
    /// Tasks
    /// </summary>
    public Dictionary<int, BackgroundTaskInfo> Tasks { get; set; }

    [Inject]
    public IBackgroundTaskService BackgroundTaskService { get; set; }

    private IBackgroundTaskNotificationProvider Channel;

    private PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(800));

    /// <summary>
    /// InitAsync
    /// </summary>
    public async Task InitAsync()
    {
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
    }
}
