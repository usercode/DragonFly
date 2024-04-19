// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IBackgroundTaskService
{
    Task<IBackgroundTaskInfo[]> GetTasksAsync();

    Task CancelAsync(int id);

    Task<IBackgroundTaskNotificationProvider> StartNotificationProviderAsync();
}
