// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using SmartResults;

namespace DragonFly.AspNetCore.Permissions.Storages;

public class ContentVersionPermissionStorage : IContentVersionStorage
{
    public ContentVersionPermissionStorage(IContentVersionStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
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
    private IContentVersionStorage Storage { get; }

    public async Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Read))
                                            .ThenAsync(x => Storage.GetContentByVersionAsync(schema, id));
    }

    public async Task<Result<QueryResult<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Read))
                                            .ThenAsync(x => Storage.GetContentVersionsAsync(schema, id));
    }
}
