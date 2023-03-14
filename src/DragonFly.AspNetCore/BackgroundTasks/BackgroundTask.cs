// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly;

/// <summary>
/// BackgroundTask
/// </summary>
public class BackgroundTask
{
    public BackgroundTask(long id, string name, ClaimsPrincipal? createdBy)
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
    }

    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; }

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
    public DateTimeOffset? StartedAt { get; internal set; }

    /// <summary>
    /// ExitAt
    /// </summary>
    public DateTimeOffset? ExitAt { get; internal set; }

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
    public string Status { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public Task? Task { get; internal set; }

    /// <summary>
    /// State
    /// </summary>
    public BackgroundTaskState State { get; internal set; }

    /// <summary>
    /// CancellationToken
    /// </summary>
    public CancellationTokenSource CancellationTokenSource { get; }

    /// <summary>
    /// Exception
    /// </summary>
    public Exception? Exception { get; private set; }

    /// <summary>
    /// SetException
    /// </summary>
    /// <param name="exception"></param>
    public void SetException(Exception exception)
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
    public void SetCanceling()
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
    public void SetCanceled()
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
    public void SetCompleted()
    {
        if (State != BackgroundTaskState.Running)
        {
            throw new Exception();
        }

        ExitAt = DateTimeOffset.Now;
        State = BackgroundTaskState.Completed;
        ProgressValue = ProgressMaxValue;
    }
}
