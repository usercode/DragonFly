// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly;

public interface IBackgroundTaskService
{
    Task<Result<BackgroundTaskInfo[]>> GetTasksAsync();

    Task<Result> CancelAsync(int id);

    Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync();
}
