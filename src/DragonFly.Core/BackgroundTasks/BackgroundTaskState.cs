// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public enum BackgroundTaskState
{
    Awaiting,
    Running,
    Completed,
    Failed,
    Canceling,
    Canceled
}
