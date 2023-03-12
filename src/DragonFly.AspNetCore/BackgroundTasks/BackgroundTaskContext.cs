﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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

    public void SetProgress(long value)
    {
        Task.ProgressValue = value;
    }

    public void IncrementProgressValue()
    {
        Task.ProgressValue++;
    }

    public void SetProgressMaxValue(long max)
    {
        Task.ProgressMaxValue = max;
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
