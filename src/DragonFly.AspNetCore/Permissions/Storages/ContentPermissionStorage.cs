// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using SmartResults;

namespace DragonFly.AspNetCore.Permissions;

public class ContentPermissionStorage : IContentStorage
{
    public ContentPermissionStorage(IContentStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
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
    private IContentStorage Storage { get; }

    public async Task<Result> CreateAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Create)).ThenAsync(x => Storage.CreateAsync(content));
    }

    public async Task<Result<bool>> DeleteAsync(ContentId id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(id.Schema, ContentAction.Delete)).ThenAsync(x => Storage.DeleteAsync(id));
    }

    public async Task<Result<ContentItem?>> GetContentAsync(ContentId id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(id.Schema, ContentAction.Read)).ThenAsync(x => Storage.GetContentAsync(id));
    }

    public async Task<Result<bool>> PublishAsync(ContentId id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(id.Schema, ContentAction.Publish)).ThenAsync(x => Storage.PublishAsync(id));
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Publish)).ThenAsync(x => Storage.PublishQueryAsync(query));
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Query)).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result<bool>> UnpublishAsync(ContentId id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(id.Schema, ContentAction.Unpublish)).ThenAsync(x => Storage.UnpublishAsync(id));
    }

    public async Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Unpublish)).ThenAsync(x => Storage.UnpublishQueryAsync(query));
    }

    public async Task<Result> UpdateAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Update)).ThenAsync(x => Storage.UpdateAsync(content));
    }
}
