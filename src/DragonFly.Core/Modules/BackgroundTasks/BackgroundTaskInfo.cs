﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BackgroundTaskInfo
/// </summary>
public class BackgroundTaskInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// CreatedAt
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// CreatedBy
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Progress
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
    /// State
    /// </summary>
    public BackgroundTaskState State { get; set; }

    /// <summary>
    /// ParentTaskId
    /// </summary>
    public long? ParentTaskId { get; set; }
}
