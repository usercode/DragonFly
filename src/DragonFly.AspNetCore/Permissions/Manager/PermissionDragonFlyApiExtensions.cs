// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;

namespace DragonFly;

public static class PermissionDragonFlyApiExtensions
{
    public static PermissionManager Permissions(this IDragonFlyApi api)
    {
        return PermissionManager.Default;
    }

    public static PermissionManager AddDefaults(this PermissionManager manager)
    {
        manager
            .Add(ContentPermissions.QueryContent)
            .Add(ContentPermissions.ReadContent)
            .Add(ContentPermissions.CreateContent)
            .Add(ContentPermissions.UpdateContent)
            .Add(ContentPermissions.DeleteContent)
            .Add(ContentPermissions.PublishContent)
            .Add(ContentPermissions.UnpublishContent)
            .Add(SchemaPermissions.QuerySchema)
            .Add(SchemaPermissions.ReadSchema)
            .Add(SchemaPermissions.CreateSchema)
            .Add(SchemaPermissions.UpdateSchema)
            .Add(SchemaPermissions.DeleteSchema)
            .Add(AssetPermissions.QueryAsset)
            .Add(AssetPermissions.ReadAsset)
            .Add(AssetPermissions.CreateAsset)
            .Add(AssetPermissions.UpdateAsset)
            .Add(AssetPermissions.DeleteAsset)
            .Add(AssetPermissions.UploadAsset)
            .Add(AssetPermissions.DownloadAsset)
            .Add(AssetPermissions.PublishAsset)
            .Add(AssetPermissions.UnpublishAsset)
            .Add(BackgroundTaskPermissions.QueryBackgroundTask)
            .Add(BackgroundTaskPermissions.CancelBackgroundTask)
            .Add(WebHookPermissions.QueryWebHook)
            .Add(WebHookPermissions.ReadWebHook)
            .Add(WebHookPermissions.CreateWebHook)
            .Add(WebHookPermissions.UpdateWebHook)
            .Add(WebHookPermissions.DeleteWebHook);

        return manager;
    }
}
