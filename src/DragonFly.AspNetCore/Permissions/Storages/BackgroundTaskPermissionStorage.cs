// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Results;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class BackgroundTaskPermissionStorage : IBackgroundTaskManager
{
    public BackgroundTaskPermissionStorage(IBackgroundTaskManager storage, IDragonFlyApi api)
    {
        Storage = storage;
        Api = api;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IBackgroundTaskManager Storage { get; }

    public async Task<Result> CancelAsync(int id)
    {
        return await Api.AuthorizeAsync(BackgroundTaskPermissions.CancelBackgroundTask).ThenAsync(x => Storage.CancelAsync(id));
    }

    public async Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        return await Api.AuthorizeAsync(BackgroundTaskPermissions.QueryBackgroundTask).ThenAsync(x => Storage.GetTasksAsync());
    }

    public async Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        return await Api.AuthorizeAsync(BackgroundTaskPermissions.QueryBackgroundTask).ThenAsync(x => Storage.StartNotificationProviderAsync());
    }

    public BackgroundTask Start(string name, Func<BackgroundTaskContext, Task> action)
    {
        return Storage.Start(name, action);
    }

    public BackgroundTask Start<T>(string name, T input, Func<BackgroundTaskContext<T>, Task> action)
    {
        return Storage.Start(name, input, action);
    }  
}
