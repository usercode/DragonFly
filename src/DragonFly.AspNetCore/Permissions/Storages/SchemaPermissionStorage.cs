// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License


using DragonFly.Permissions;
using Results;

namespace DragonFly.AspNetCore.Permissions;

public class SchemaPermissionStorage : ISchemaStorage
{
    public SchemaPermissionStorage(ISchemaStorage storage, IDragonFlyApi api, IPrincipalContext principalContext)
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
    private ISchemaStorage Storage { get; }

    public async Task<Result> CreateAsync(ContentSchema schema)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.CreateSchema).ThenAsync(x => Storage.CreateAsync(schema));
    }

    public async Task<Result> DeleteAsync(ContentSchema schema)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.DeleteSchema).ThenAsync(x => Storage.DeleteAsync(schema));
    }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(Guid id)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.ReadSchema).ThenAsync(x => Storage.GetSchemaAsync(id));
    }

    public async Task<Result<ContentSchema?>> GetSchemaAsync(string name)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.ReadSchema).ThenAsync(x => Storage.GetSchemaAsync(name));
    }

    public async Task<Result<QueryResult<ContentSchema>>> QuerySchemasAsync()
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.QuerySchema).ThenAsync(x => Storage.QuerySchemasAsync());
    }

    public async Task<Result> UpdateAsync(ContentSchema schema)
    {
        return await Api.AuthorizeAsync(PrincipalContext.Current, SchemaPermissions.UpdateSchema).ThenAsync(x => Storage.UpdateAsync(schema));
    }
}
