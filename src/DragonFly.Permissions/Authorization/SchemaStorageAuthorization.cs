// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Content;

/// <summary>
/// SchemaStorageAuthorization
/// </summary>
public class SchemaStorageAuthorization : ISchemaStorage
{
    public SchemaStorageAuthorization(
        ISchemaStorage storage,
        IDragonFlyApi api)
    {
        Api = api;
        Storage = storage;
    }

    /// <summary>
    /// Storage
    /// </summary>
    public ISchemaStorage Storage { get; }

    /// <summary>
    /// Api
    /// </summary>
    public IDragonFlyApi Api { get; }

    public async Task CreateAsync(ContentSchema contentType)
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaCreate);

        await Storage.CreateAsync(contentType);
    }

    public async Task<ContentSchema> GetSchemaAsync(Guid id)
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaRead);

        return await Storage.GetSchemaAsync(id);
    }

    public async Task<ContentSchema> GetSchemaAsync(string name)
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaRead);

        return await Storage.GetSchemaAsync(name);
    }

    public async Task<QueryResult<ContentSchema>> QuerySchemasAsync()
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaQuery);

        return await Storage.QuerySchemasAsync();
    }

    public async Task UpdateAsync(ContentSchema entity)
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaUpdate);

        await Storage.UpdateAsync(entity);
    }

    public async Task DeleteAsync(ContentSchema entity)
    {
        await Api.AuthorizeAsync(SchemaPermissions.SchemaDelete);

        await Storage.DeleteAsync(entity);
    }
}
