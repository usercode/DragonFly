// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using SmartResults;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class BackgroundTaskPermissionStorage : IBackgroundTaskManager
{
    public BackgroundTaskPermissionStorage(IBackgroundTaskManager storage, IDragonFlyApi api, IPrincipalContext principalContext)
    {
        Storage = storage;
        Api = api;
        PrincipalContext = principalContext;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    private IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IBackgroundTaskManager Storage { get; }

    public async Task<Result> CancelAsync(int id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, BackgroundTaskPermissions.CancelBackgroundTask)
                        .ThenAsync(x => Storage.CancelAsync(id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<BackgroundTaskInfo[]>> GetTasksAsync()
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, BackgroundTaskPermissions.QueryBackgroundTask)
                        .ThenAsync(x => Storage.GetTasksAsync())
                        .ConfigureAwait(false);
    }

    public async Task<Result<IBackgroundTaskNotificationProvider>> StartNotificationProviderAsync()
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, BackgroundTaskPermissions.QueryBackgroundTask)
                        .ThenAsync(x => Storage.StartNotificationProviderAsync())
                        .ConfigureAwait(false);
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
