﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DragonFly.AspNetCore;

/// <summary>
/// BackgroundTaskManager
/// </summary>
public class BackgroundTaskManager : IBackgroundTaskManager
{
    public BackgroundTaskManager(IHubContext<BackgroundTaskHub> hub, IServiceProvider serviceProvider, ILogger<BackgroundTaskManager> logger)
    {
        Hub = hub;
        ServiceProvider = serviceProvider;
        Logger = logger;
    }

    /// <summary>
    /// Hub
    /// </summary>
    public IHubContext<BackgroundTaskHub> Hub { get; }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Logger
    /// </summary>
    private ILogger<BackgroundTaskManager> Logger { get; }

    private readonly IDictionary<int, BackgroundTask> Tasks = new Dictionary<int, BackgroundTask>();

    private int _nextId = 1;
    private object _syncObject = new object();

    private async Task TaskHasChangedAsync(BackgroundTaskStatusChange state, BackgroundTask task)
    {
        await Hub.Clients.All.SendAsync("TaskChanged", state, task.ToTaskInfo());
    }

    public BackgroundTask Start(string name, Func<BackgroundTaskContext, Task> action)
    {
        return Start(name, 0, action);
    }

    public BackgroundTask Start<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action)
    {
        lock (_syncObject)
        {
            BackgroundTask backgroundTask = new BackgroundTask(_nextId++, name, PermissionPrincipal.GetCurrent(), BackgroundTask.GetCurrentTask());

            backgroundTask.Task = Task.Run(async () =>
            {
                BackgroundTaskContext<T> ctx = new BackgroundTaskContext<T>(backgroundTask, ServiceProvider, input, backgroundTask.CancellationTokenSource.Token);
                ctx.StateChanged += () => TaskHasChangedAsync(BackgroundTaskStatusChange.Updated, backgroundTask);

                try
                {
                    BackgroundTask.SetCurrentTask(backgroundTask);

                    //notify new task
                    await TaskHasChangedAsync(BackgroundTaskStatusChange.Added, backgroundTask);

                    //wait before starting task
                    await Task.Delay(TimeSpan.FromSeconds(3));

                    Logger.LogInformation("Background task is started: {id} / {name}", backgroundTask.Id, backgroundTask.Name);

                    //set state to running
                    backgroundTask.SetRunning();

                    await TaskHasChangedAsync(BackgroundTaskStatusChange.Updated, backgroundTask);

                    //execute task
                    await action(ctx);

                    //is task canceled or completed?
                    if (backgroundTask.State == BackgroundTaskState.Canceling)
                    {
                        backgroundTask.SetCanceled();
                    }
                    else
                    {
                        backgroundTask.SetCompleted();
                    }
                }
                catch (TaskCanceledException)
                {
                    backgroundTask.SetCanceled();
                }
                catch (Exception ex)
                {
                    backgroundTask.SetException(ex);
                }
                finally
                {
                    Logger.LogInformation("Background task is exited: {id} / {name}", backgroundTask.Id, backgroundTask.Name);

                    await TaskHasChangedAsync(BackgroundTaskStatusChange.Updated, backgroundTask);

                    //wait before removing task
                    await Task.Delay(TimeSpan.FromMinutes(1));

                    //remove task
                    lock (_syncObject)
                    {
                        Tasks.Remove(backgroundTask.Id);
                    }

                    await TaskHasChangedAsync(BackgroundTaskStatusChange.Removed, backgroundTask);
                }
            });

            Tasks.Add(backgroundTask.Id, backgroundTask);

            return backgroundTask;
        }
    }

    public Task<IBackgroundTaskInfo[]> GetTasksAsync()
    {
        lock (_syncObject)
        {
            return Task.FromResult(
                                    Tasks
                                    .Values
                                    .Select(x => x.ToTaskInfo())
                                    .OrderBy(x => x.Id)
                                    .Cast<IBackgroundTaskInfo>()
                                    .ToArray());
        }
    }

    public Task CancelAsync(int id)
    {
        lock (_syncObject)
        {
            if (Tasks.TryGetValue(id, out BackgroundTask? task))
            {
                task.SetCanceling();
            }

            return Task.CompletedTask;
        }
    }
}
