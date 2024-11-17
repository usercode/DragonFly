// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.SignalR;
using SmartResults;

namespace DragonFly.AspNetCore;

/// <summary>
/// BackgroundTaskManager
/// </summary>
public class BackgroundTaskManager : IBackgroundTaskManager
{
    public BackgroundTaskManager(IPrincipalContext principalContext, IServiceProvider serviceProvider, ILogger<BackgroundTaskManager> logger)
    {
        ServiceProvider = serviceProvider;
        PrincipalContext = principalContext;
        Logger = logger;
    }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    public IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// Logger
    /// </summary>
    private ILogger<BackgroundTaskManager> Logger { get; }

    private readonly Dictionary<int, BackgroundTask> Tasks = new ();

    /// <summary>
    /// BackgroundTaskChanged
    /// </summary>
    public event Func<BackgroundTaskStatusChange, BackgroundTaskInfo, Task>? BackgroundTaskChanged;

    private int _nextId = 1;
    private object _syncObject = new object();

    private async Task TaskHasChangedAsync(BackgroundTaskStatusChange state, BackgroundTaskInfo task)
    {
        if (BackgroundTaskChanged != null)
        {
            await BackgroundTaskChanged(state, task);
        }
    }

    public BackgroundTask Start(string name, Func<BackgroundTaskContext, Task> action)
    {
        return Start(name, 0, action);
    }

    public BackgroundTask Start<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action)
    {
        lock (_syncObject)
        {
            BackgroundTask backgroundTask = new BackgroundTask(_nextId++, name, PrincipalContext.Current, BackgroundTask.Current);

            backgroundTask.Task = Task.Factory.StartNew(async () =>
            {
                BackgroundTaskContext<T> ctx = new BackgroundTaskContext<T>(backgroundTask, ServiceProvider, input, backgroundTask.CancellationTokenSource.Token);
                ctx.StateChanged += () => TaskHasChangedAsync(BackgroundTaskStatusChange.Updated, backgroundTask);

                try
                {
                    BackgroundTask.Current = backgroundTask;

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
            }, TaskCreationOptions.LongRunning);

            Tasks.Add(backgroundTask.Id, backgroundTask);

            return backgroundTask;
        }
    }

    public Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        lock (_syncObject)
        {
            return Task.FromResult(
                        Result.Ok(
                                    Tasks
                                    .Values
                                    .Select(x => (BackgroundTaskInfo)x)
                                    .OrderBy(x => x.Id)
                                    .ToArray()));
        }
    }

    public Task<Result> CancelAsync(int id)
    {
        lock (_syncObject)
        {
            if (Tasks.TryGetValue(id, out BackgroundTask? task))
            {
                task.SetCanceling();
            }

            return Task.FromResult(Result.Ok());
        }
    }

    public Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        return Task.FromResult<Result<IBackgroundTaskNotificationProvider>>(new LocalBackgroundTaskNotificationProvider(this));
    }
}
