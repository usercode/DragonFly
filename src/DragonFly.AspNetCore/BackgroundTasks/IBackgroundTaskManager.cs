// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IBackgroundTaskManager : IBackgroundTaskService
{
    BackgroundTask StartNew(string name, Func<BackgroundTaskContext, Task> action);

    BackgroundTask StartNew<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action);
}
