// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using Results;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class WebHookPermissionStorage : IWebHookStorage
{
    public WebHookPermissionStorage(IWebHookStorage storage, IDragonFlyApi api)
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
    private IWebHookStorage Storage { get; }

    public async Task<Result> CreateAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(WebHookPermissions.CreateWebHook).ThenAsync(x => Storage.CreateAsync(webHook));
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(WebHookPermissions.DeleteWebHook).ThenAsync(x => Storage.DeleteAsync(webHook));
    }

    public async Task<Result<WebHook?>> GetAsync(Guid id)
    {
        return await Api.AuthorizeAsync(WebHookPermissions.ReadWebHook).ThenAsync(x => Storage.GetAsync(id));
    }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        return await Api.AuthorizeAsync(WebHookPermissions.QueryWebHook).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result> UpdateAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(WebHookPermissions.UpdateWebHook).ThenAsync(x => Storage.UpdateAsync(webHook));
    }
}
