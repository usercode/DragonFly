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
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Create))
                        .ThenAsync(x => Storage.CreateAsync(content))
                        .ConfigureAwait(false);
    }

    public async Task<Result<bool>> DeleteAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Delete))
                        .ThenAsync(x => Storage.DeleteAsync(schema, id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Read))
                        .ThenAsync(x => Storage.GetContentAsync(schema, id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<ContentReferenceIndex>> GetReferencedByAsync(string schema, Guid id)
    {
        //return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Query)).ThenAsync(x => Storage.GetReferencedByAsync(schema, id));

        return await Storage.GetReferencedByAsync(schema, id);
    }

    public async Task<Result<bool>> PublishAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Publish))
                        .ThenAsync(x => Storage.PublishAsync(schema, id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Publish))
                        .ThenAsync(x => Storage.PublishQueryAsync(query))
                        .ConfigureAwait(false);
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Query))
                        .ThenAsync(x => Storage.QueryAsync(query))
                        .ConfigureAwait(false);
    }

    public async Task<Result<bool>> UnpublishAsync(string schema, Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(schema, ContentAction.Unpublish))
                        .ThenAsync(x => Storage.UnpublishAsync(schema, id))
                        .ConfigureAwait(false);
    }

    public async Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(query.Schema, ContentAction.Unpublish))
                        .ThenAsync(x => Storage.UnpublishQueryAsync(query))
                        .ConfigureAwait(false);
    }

    public async Task<Result> UpdateAsync(ContentItem content)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, ContentPermissions.Create(content.Schema.Name, ContentAction.Update))
                        .ThenAsync(x => Storage.UpdateAsync(content))
                        .ConfigureAwait(false);
    }

    public async Task<Result> RebuildDatabaseAsync()
    {
       return await Storage.RebuildDatabaseAsync().ConfigureAwait(false);
    }
}
