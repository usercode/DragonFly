// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IBackgroundTaskService
{
    Task<IEnumerable<BackgroundTaskInfo>> GetTasksAsync();

    Task CancelAsync(int id);
}
