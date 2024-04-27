// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using DragonFly.Permissions;

namespace DragonFly.AspNetCore;

/// <summary>
/// CreateContentPermissionsInitializer
/// </summary>
public class CreateContentPermissionsInitializer : IPostInitialize
{
    public CreateContentPermissionsInitializer(ISchemaStorage schemaStorage)
    {
        SchemaStorage = schemaStorage;
    }

    /// <summary>
    /// SchemaStorage
    /// </summary>
    private ISchemaStorage SchemaStorage { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        QueryResult<ContentSchema> result = await SchemaStorage.QuerySchemasAsync();

        foreach (ContentSchema schema in result.Items)
        {
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Manage));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Query));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Read));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Create));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Update));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Delete));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Publish));
            api.Permission().Add(ContentPermissions.Create(schema.Name, ContentAction.Unpublish));
        }
    }
}
