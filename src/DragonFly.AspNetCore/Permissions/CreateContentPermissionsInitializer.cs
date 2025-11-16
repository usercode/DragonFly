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
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Manage));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Query));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Read));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Create));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Update));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Delete));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Publish));
            api.Permissions.Add(ContentPermissions.Create(schema.Name, ContentAction.Unpublish));
        }
    }
}
