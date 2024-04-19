// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;

namespace DragonFly;

public sealed class LocalBackgroundTaskNotificationProvider : IBackgroundTaskNotificationProvider
{
    public LocalBackgroundTaskNotificationProvider(BackgroundTaskManager manager)
    {
        Manager = manager;
        Manager.BackgroundTaskChanged += Manager_BackgroundTaskChanged;
    }

    /// <summary>
    /// Manager
    /// </summary>
    private BackgroundTaskManager Manager { get; }

    /// <summary>
    /// BackgroundTaskChanged
    /// </summary>
    public event BackgroundTaskHandler? BackgroundTaskChanged;

    private async Task Manager_BackgroundTaskChanged(BackgroundTaskStatusChange arg1, IBackgroundTaskInfo arg2)
    {
        if (BackgroundTaskChanged != null)
        {
            await BackgroundTaskChanged.Invoke(arg1, arg2);
        }
    }

    public ValueTask DisposeAsync()
    {
        Manager.BackgroundTaskChanged -= Manager_BackgroundTaskChanged;

        return ValueTask.CompletedTask;
    }
}
