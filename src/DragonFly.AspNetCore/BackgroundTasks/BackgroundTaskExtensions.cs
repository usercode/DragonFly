// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore;

public static class BackgroundTaskExtensions
{
    public static BackgroundTaskInfo ToTaskInfo(this BackgroundTask task)
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
