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

    private readonly IDictionary<long, BackgroundTask> Tasks = new  Dictionary<long, BackgroundTask>();

    private long _nextId = 0;
    private object _syncObject = new object();

    /// <summary>
    /// StartNew
    /// </summary>
    /// <param name="task"></param>
    public BackgroundTask StartNew(string name, Func<BackgroundTaskContext, Task> action)
    {
        return StartNew(name, 0, (ctx) => action(ctx));
    }

    public BackgroundTask StartNew<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action)
    {
        lock (_syncObject)
        {
            BackgroundTask backgroundTask = new BackgroundTask(_nextId++, name, PermissionState.GetPrincipal());

            backgroundTask.Task = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));

                    Logger.LogInformation("Background task is started: {name}", backgroundTask.Name);

                    backgroundTask.StartedAt = DateTimeOffset.Now;
                    backgroundTask.State = BackgroundTaskState.Running;

                    BackgroundTaskContext<T> ctx = new BackgroundTaskContext<T>(backgroundTask, ServiceProvider, input, backgroundTask.CancellationTokenSource.Token);
                    ctx.StateChanged += () => Hub.Clients.All.SendAsync("TaskChanged");

                    await action(ctx);

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

                    await Hub.Clients.All.SendAsync("TaskChanged");

                    await Task.Delay(TimeSpan.FromMinutes(10));

                    lock (_syncObject)
                    {
                        Tasks.Remove(backgroundTask.Id);
                    }
                }
            });

            Tasks.Add(backgroundTask.Id, backgroundTask);

            return backgroundTask;
        }
    }

    private void Ctx_StateChanged()
    {
        throw new NotImplementedException();
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
                        Status = x.Status,
                        State = x.State,
                        Exception = x.Exception?.Message
                    })
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
