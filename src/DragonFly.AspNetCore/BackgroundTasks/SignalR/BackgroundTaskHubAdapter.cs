// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.SignalR;

namespace DragonFly.AspNetCore.BackgroundTasks.SignalR;

public class BackgroundTaskHubAdapter
{
    public BackgroundTaskHubAdapter(IHubContext<BackgroundTaskHub> hub, BackgroundTaskManager manager)
    {
        manager.BackgroundTaskChanged += async (state, task) =>
        {
            await hub.Clients.All.SendAsync("TaskChanged", state, task);
        };
    }
}
