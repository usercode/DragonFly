﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore;

/// <summary>
/// BackgroundTask
/// </summary>
public class BackgroundTask
{
    private static readonly AsyncLocal<BackgroundTask?> _currentTask = new AsyncLocal<BackgroundTask?>();

    public static BackgroundTask? Current
    {
        get => _currentTask.Value;
        set => _currentTask.Value = value;
    }

    public BackgroundTask(int id, string name, ClaimsPrincipal? createdBy, BackgroundTask? parentTask)
    {
        Id = id;
        Name = name;
        ProgressValue = 0;
        ProgressMaxValue = 100;
        CreatedAt = DateTimeOffset.Now;
        CreatedBy = createdBy;
        Status = string.Empty;
        State = BackgroundTaskState.Awaiting;
        CancellationTokenSource = new CancellationTokenSource();
        ParentTask = parentTask;
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// CreatedAt
    /// </summary>
    public DateTimeOffset? CreatedAt { get; }

    /// <summary>
    /// CreatedBy
    /// </summary>
    public ClaimsPrincipal? CreatedBy { get; }

    /// <summary>
    /// StartedAt
    /// </summary>
    public DateTimeOffset? StartedAt { get; private set; }

    /// <summary>
    /// ExitAt
    /// </summary>
    public DateTimeOffset? ExitAt { get; private set; }

    /// <summary>
    /// ProgressValue
    /// </summary>
    public long ProgressValue { get; set; }

    /// <summary>
    /// ProgressMaxValue
    /// </summary>
    public long ProgressMaxValue { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public Task? Task { get; internal set; }

    /// <summary>
    /// State
    /// </summary>
    public BackgroundTaskState State { get; private set; }

    /// <summary>
    /// CancellationToken
    /// </summary>
    public CancellationTokenSource CancellationTokenSource { get; }

    /// <summary>
    /// Exception
    /// </summary>
    public Exception? Exception { get; private set; }

    /// <summary>
    /// ParentTask
    /// </summary>
    public BackgroundTask? ParentTask { get; set; }

    public long? ParentTaskId => ParentTask?.Id;

    /// <summary>
    /// SetRunning
    /// </summary>
    internal void SetRunning()
    {
        StartedAt = DateTimeOffset.Now;
        State = BackgroundTaskState.Running;
    }

    /// <summary>
    /// SetException
    /// </summary>
    /// <param name="exception"></param>
    internal void SetException(Exception exception)
    {
        if (State != BackgroundTaskState.Running)
        {
            throw new Exception();
        }

        ExitAt = DateTimeOffset.Now;
        State = BackgroundTaskState.Failed;
        Exception = exception;
    }

    /// <summary>
    /// SetCanceling
    /// </summary>
    internal void SetCanceling()
    {
        if (State != BackgroundTaskState.Running)
        {
            throw new Exception();
        }

        State = BackgroundTaskState.Canceling;
        CancellationTokenSource.Cancel();
    }

    /// <summary>
    /// SetCanceled
    /// </summary>
    internal void SetCanceled()
    {
        if (State != BackgroundTaskState.Canceling)
        {
            throw new Exception();
        }

        ExitAt = DateTimeOffset.Now;
        State = BackgroundTaskState.Canceled;
    }

    /// <summary>
    /// SetCompleted
    /// </summary>
    internal void SetCompleted()
    {
        if (State != BackgroundTaskState.Running)
        {
            throw new Exception();
        }

        ExitAt = DateTimeOffset.Now;
        State = BackgroundTaskState.Completed;
        ProgressValue = ProgressMaxValue;
    }

    public static implicit operator BackgroundTaskInfo(BackgroundTask task)
    {
        return new BackgroundTaskInfo()
        {
            Id = task.Id,
            CreatedAt = task.CreatedAt,
            CreatedBy = task.CreatedBy?.FindFirstValue("Name"),
            Name = task.Name,
            ProgressValue = task.ProgressValue,
            ProgressMaxValue = task.ProgressMaxValue,
            Status = task.Exception?.Message ?? task.Status,
            State = task.State,
            ParentTaskId = task.ParentTask?.Id
        };
    }
}
