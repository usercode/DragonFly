// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public delegate Task BackgroundTaskHandler(BackgroundTaskStatusChange change, BackgroundTaskInfo taskInfo);

public interface IBackgroundTaskNotificationProvider : IAsyncDisposable
{
    public event BackgroundTaskHandler? BackgroundTaskChanged;
}
