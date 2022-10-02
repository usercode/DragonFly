// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using DragonFly.Query;
using DragonFly.Storage;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Content;

/// <summary>
/// ContentStorageAuthorization
/// </summary>
public class ContentStorageAuthorization : IContentStorage
{
    public ContentStorageAuthorization(
        IContentStorage storage,
        IDragonFlyApi api)
    {
        Api = api;
        Storage = storage;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public IContentStorage Storage { get; }

    /// <summary>
    /// Api
    /// </summary>
    public IDragonFlyApi Api { get; }

    public async Task CreateAsync(ContentItem contentItem)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentCreate);

        await Storage.CreateAsync(contentItem);
    }

    public async Task DeleteAsync(string schema, Guid id)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentDelete);

        await Storage.DeleteAsync(schema, id);
    }

    public async Task<ContentItem> GetContentAsync(string schema, Guid id)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentRead);

        return await Storage.GetContentAsync(schema, id);
    }

    public async Task PublishAsync(string schema, Guid id)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentPublish);

        await Storage.PublishAsync(schema, id);
    }

    public async Task PublishQueryAsync(ContentItemQuery query)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentQuery);
        await Api.AuthorizeAsync(ContentPermissions.ContentPublish);

        await Storage.PublishQueryAsync(query);
    }

    public async Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery query)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentQuery);

        return await Storage.QueryAsync(query);
    }

    public async Task UnpublishAsync(string schema, Guid id)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentUnpublish);

        await Storage.UnpublishAsync(schema, id);
    }

    public async Task UpdateAsync(ContentItem entity)
    {
        await Api.AuthorizeAsync(ContentPermissions.ContentUpdate);

        await Storage.UpdateAsync(entity);
    }
}
