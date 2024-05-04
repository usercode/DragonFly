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

    public async Task<Result> DeleteAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Delete)).ThenAsync(x => Storage.DeleteAsync(content));
    }

    public async Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Read)).ThenAsync(x => Storage.GetContentAsync(schema, id));
    }

    public async Task<Result> PublishAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Publish)).ThenAsync(x => Storage.PublishAsync(content));
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Publish)).ThenAsync(x => Storage.PublishQueryAsync(query));
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Query)).ThenAsync(x => Storage.QueryAsync(query));
    }

    public async Task<Result> UnpublishAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Unpublish)).ThenAsync(x => Storage.UnpublishAsync(content));
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
