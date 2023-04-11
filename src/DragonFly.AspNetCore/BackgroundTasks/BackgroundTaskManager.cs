// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using DragonFly.AspNetCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DragonFly;

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

    private readonly IDictionary<long, BackgroundTask> Tasks = new Dictionary<long, BackgroundTask>();

    private long _nextId = 1;
    private object _syncObject = new object();

    private async Task TaskHasChangedAsnyc()
    {
        await Hub.Clients.All.SendAsync("TaskChanged");
    }

    public BackgroundTask Start(string name, Func<BackgroundTaskContext, Task> action)
    {
        return Start(name, 0, (ctx) => action(ctx));
    }

    public BackgroundTask Start<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action)
    {
        lock (_syncObject)
        {
            BackgroundTask backgroundTask = new BackgroundTask(_nextId++, name, Permission.GetCurrentPrincipal(), BackgroundTask.GetCurrentTask());

            backgroundTask.Task = Task.Run(async () =>
            {
                BackgroundTaskContext<T> ctx = new BackgroundTaskContext<T>(backgroundTask, ServiceProvider, input, backgroundTask.CancellationTokenSource.Token);
                ctx.StateChanged += () => TaskHasChangedAsnyc();

                try
                {
                    BackgroundTask.SetCurrentTask(backgroundTask);

                    //notify new task
                    await TaskHasChangedAsnyc();

                    //wait before starting task
                    await Task.Delay(TimeSpan.FromSeconds(2));

                    Logger.LogInformation("Background task is started: {id} / {name}", backgroundTask.Id, backgroundTask.Name);

                    //set state to running
                    backgroundTask.SetRunning();

                    await TaskHasChangedAsnyc();

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

                    await TaskHasChangedAsnyc();

                    //wait before removing task
                    await Task.Delay(TimeSpan.FromMinutes(1));

                    //remove task
                    lock (_syncObject)
                    {
                        Tasks.Remove(backgroundTask.Id);
                    }

                    await TaskHasChangedAsnyc();
                }
            });

            Tasks.Add(backgroundTask.Id, backgroundTask);

            return backgroundTask;
        }
    }

    public Task<IEnumerable<BackgroundTaskInfo>> GetTasksAsync()
    {
        lock (_syncObject)
        {
            return Task.FromResult<IEnumerable<BackgroundTaskInfo>>(
                    Tasks
                    .Values
                    .Select(x => new BackgroundTaskInfo()
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy?.FindFirstValue("Name"),
                        Name = x.Name,
                        ProgressValue = x.ProgressValue,
                        ProgressMaxValue = x.ProgressMaxValue,
                        Status = x.Exception?.Message ?? x.Status,
                        State = x.State,
                        ParentTaskId = x.ParentTask?.Id
                    })
                    .OrderBy(x => x.Id)
                    .ToList());
        }
    }

    public Task CancelAsync(long id)
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
