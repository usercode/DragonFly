// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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

    public async Task SetProgressAsync(long value)
    {
        Task.ProgressValue = value;

        await StateChanged?.Invoke();
    }

    public async Task IncrementProgressValueAsync()
    {
        Task.ProgressValue++;

        await StateChanged?.Invoke();
    }

    public async Task SetProgressMaxValueAsync(long max)
    {
        Task.ProgressMaxValue = max;

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
