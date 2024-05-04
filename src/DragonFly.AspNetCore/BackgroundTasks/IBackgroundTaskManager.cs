// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly.AspNetCore;

public interface IBackgroundTaskManager : IBackgroundTaskService
{
    /// <summary>
    /// Starts a new background task.
    /// </summary>
    BackgroundTask Start(string name, Func<BackgroundTaskContext, Task> action);

    /// <summary>
    /// Starts a new background task with input.
    /// </summary>
    BackgroundTask Start<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action);
}
