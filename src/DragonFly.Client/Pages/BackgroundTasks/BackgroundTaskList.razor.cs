// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DragonFly.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DragonFly.Client.Pages.BackgroundTasks;

public class BackgroundTaskListBase : StartComponentBase
{
    [Inject]
    public IBackgroundTaskService BackgroundTaskService { get; set; }

    public IEnumerable<BackgroundTaskInfo> Tasks { get; set; }

    protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
    {
        toolbarItems.AddRefreshButton(this);
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
}
