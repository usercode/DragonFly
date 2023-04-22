// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IBackgroundTaskInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// CreatedAt
    /// </summary>
    public DateTimeOffset? CreatedAt { get; }

    /// <summary>
    /// CreatedBy
    /// </summary>
    public string? CreatedBy { get; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Progress
    /// </summary>
    public long ProgressValue { get; }

    /// <summary>
    /// ProgressMaxValue
    /// </summary>
    public long ProgressMaxValue { get; }

    /// <summary>
    /// Status
    /// </summary>
    public string? Status { get; }

    /// <summary>
    /// State
    /// </summary>
    public BackgroundTaskState State { get; }

    /// <summary>
    /// ParentTaskId
    /// </summary>
    public long? ParentTaskId { get; }
}
