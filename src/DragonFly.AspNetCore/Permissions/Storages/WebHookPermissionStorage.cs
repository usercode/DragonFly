// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using SmartResults;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class WebHookPermissionStorage : IWebHookStorage
{
    public WebHookPermissionStorage(IWebHookStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
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
    private IWebHookStorage Storage { get; }

    public async Task<Result> CreateAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, WebHookPermissions.CreateWebHook).ThenAsync(x => Storage.CreateAsync(webHook));
    }

    public async Task<Result> DeleteAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, WebHookPermissions.DeleteWebHook).ThenAsync(x => Storage.DeleteAsync(webHook));
    }

    public async Task<Result<WebHook?>> GetAsync(Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, WebHookPermissions.ReadWebHook).ThenAsync(x => Storage.GetAsync(id));
    }

    public async Task<Result<QueryResult<WebHook>>> QueryAsync(WebHookQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, WebHookPermissions.QueryWebHook).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result> UpdateAsync(WebHook webHook)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, WebHookPermissions.UpdateWebHook).ThenAsync(x => Storage.UpdateAsync(webHook));
    }
}
