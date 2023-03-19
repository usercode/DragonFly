// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DragonFly;

/// <summary>
/// BackgroundTaskContext
/// </summary>
public class BackgroundTaskContext
{
    public BackgroundTaskContext(BackgroundTask task, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        Task = task;
        ServiceProvider = serviceProvider;
        CancellationToken = cancellationToken;
    }

    /// <summary>
    /// Task
    /// </summary>
    public BackgroundTask Task { get; }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// CancellationToken
    /// </summary>
    public CancellationToken CancellationToken { get; }

    public event Func<Task>? StateChanged;

    public async Task UpdateAsync(string? status, long? progressValue = null, long? maxProgressValue = null)
    {
        Task.Status = status;

        if (progressValue != null)
        {
            Task.ProgressValue = progressValue.Value;
        }

        if (maxProgressValue != null)
        {
            Task.ProgressMaxValue = maxProgressValue.Value;
        }

        await StateHasChanged();
    }

    public async Task UpdateAsync(Action<BackgroundTask> action)
    {
        action(Task);

        await StateHasChanged();
    }

    private async Task StateHasChanged()
    {
        await StateChanged?.Invoke();
    }
}

/// <summary>
/// BackgroundTaskContext
/// </summary>
public class BackgroundTaskContext<T> : BackgroundTaskContext
{
    public BackgroundTaskContext(BackgroundTask task, IServiceProvider serviceProvider, T input, CancellationToken cancellationToken)
        : base(task, serviceProvider, cancellationToken)
    {
        Input = input;
    }

    /// <summary>
    /// Input
    /// </summary>
    public T Input { get; }
}
