// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;

namespace DragonFly;

public static class PermissionDragonFlyApiExtensions
{
    public static PermissionManager Permission(this IDragonFlyApi api)
    {
        return PermissionManager.Default;
    }

    public static IPermissionItem AddDefaults(this PermissionManager manager)
    {
        manager
            .Add(GeneralPermissions.Admin, childs: x => x
                    .Add(ContentPermissions.Content, x => x
                                .Add(ContentPermissions.ContentRead, description: "Read content", sortkey: 0, childs: x => x
                                            .Add(ContentPermissions.ContentQuery, description: "Query content"))
                                .Add(ContentPermissions.ContentCreate, description: "Create content", sortkey: 1)
                                .Add(ContentPermissions.ContentUpdate, description: "Update content", sortkey: 2)
                                .Add(ContentPermissions.ContentDelete, description: "Delete content", sortkey: 3)
                                .Add(ContentPermissions.ContentPublish, description: "Publish content", sortkey: 4)
                                .Add(ContentPermissions.ContentUnpublish, description: "Unpublish content", sortkey: 5)
                                )
                    .Add(SchemaPermissions.Schema, x => x
                                .Add(SchemaPermissions.SchemaRead, description: "Read schema", childs: x => x
                                            .Add(SchemaPermissions.SchemaQuery, description: "Query schema"))
                                .Add(SchemaPermissions.SchemaCreate, description: "Create schema")
                                .Add(SchemaPermissions.SchemaUpdate, description: "Update schema")
                                .Add(SchemaPermissions.SchemaDelete, description: "Delete schema")
                                )
                    .Add(AssetPermissions.Asset, x => x
                                .Add(AssetPermissions.AssetRead, description: "Read asset", sortkey: 0, childs: x => x
                                            .Add(AssetPermissions.AssetQuery, description: "Query asset"))
                                .Add(AssetPermissions.AssetCreate, description: "Create asset", sortkey: 1)
                                .Add(AssetPermissions.AssetUpdate, description: "Update asset", sortkey: 2)
                                .Add(AssetPermissions.AssetDelete, description: "Delete asset", sortkey: 3)
                                .Add(AssetPermissions.AssetUpload, description: "Upload asset", sortkey: 4)
                                .Add(AssetPermissions.AssetDownload, description: "Download asset", sortkey: 5)
                                .Add(AssetPermissions.AssetPublish, description: "Publish asset", sortkey: 6)
                                .Add(AssetPermissions.AssetUnpublish, description: "Unpublish asset", sortkey: 7)
                                )
                    .Add(BackgroundTaskPermissions.BackgroundTask, x => x
                                .Add(BackgroundTaskPermissions.BackgroundTaskQuery, description: "Query background task", sortkey: 1)
                                .Add(BackgroundTaskPermissions.BackgroundTaskCancel, description: "Cancel background task", sortkey: 2)
                                )
                    .Add(WebHookPermissions.WebHook, x => x
                                .Add(WebHookPermissions.WebHookRead, description: "Read webhook", sortkey: 1, childs : x => x
                                    .Add(WebHookPermissions.WebHookQuery, description: "Query webhook", sortkey: 1))
                                .Add(WebHookPermissions.WebHookCreate, description: "Create webhook", sortkey: 1)
                                .Add(WebHookPermissions.WebHookUpdate, description: "Update webhook", sortkey: 2)
                                .Add(WebHookPermissions.WebHookDelete, description: "Delete webhook", sortkey: 2)
                                )
                    );

        return manager;
    }

    public static IPermissionItem Add(this IPermissionItem manager, string name, Action<IPermissionItem>? childs = null, string description = "", int sortkey = 0)
    {
        PermissionItem p = new PermissionItem(name);
        p.Description = description;
        p.SortKey = sortkey;

        p = manager.Add(p);

        childs?.Invoke(p);

        return manager;
    }       
}
