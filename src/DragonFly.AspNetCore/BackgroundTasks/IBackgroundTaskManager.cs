// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IBackgroundTaskManager : IBackgroundTaskService
{
    Task<BackgroundTask> StartAsync(string name, Func<BackgroundTaskContext, Task> action);

    Task<BackgroundTask> StartAsync<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action);
}
