// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Permissions;
using System.Security.Claims;

namespace DragonFly;

public static class DragonFlyApiPermissionExtensions
{
    extension(IDragonFlyApi api)
    {
        public PermissionManager Permissions => PermissionManager.Default;
    }

    /// <summary>
    /// Adds default permissions.
    /// </summary>
    /// <param name="manager"></param>
    /// <returns></returns>
    public static PermissionManager AddDefaults(this PermissionManager manager)
    {
        manager
            .Add(ContentPermissions.ManageContent)
            .Add(ContentPermissions.QueryContent)
            .Add(ContentPermissions.ReadContent)
            .Add(ContentPermissions.CreateContent)
            .Add(ContentPermissions.UpdateContent)
            .Add(ContentPermissions.DeleteContent)
            .Add(ContentPermissions.PublishContent)
            .Add(ContentPermissions.UnpublishContent)
            .Add(SchemaPermissions.ManageSchema)
            .Add(SchemaPermissions.QuerySchema)
            .Add(SchemaPermissions.ReadSchema)
            .Add(SchemaPermissions.CreateSchema)
            .Add(SchemaPermissions.UpdateSchema)
            .Add(SchemaPermissions.DeleteSchema)
            .Add(AssetPermissions.ManageAsset)
            .Add(AssetPermissions.QueryAsset)
            .Add(AssetPermissions.ReadAsset)
            .Add(AssetPermissions.CreateAsset)
            .Add(AssetPermissions.UpdateAsset)
            .Add(AssetPermissions.DeleteAsset)
            .Add(AssetPermissions.UploadAsset)
            .Add(AssetPermissions.DownloadAsset)
            .Add(AssetPermissions.PublishAsset)
            .Add(AssetPermissions.UnpublishAsset)
            .Add(AssetFolderPermissions.ManageAssetFolder)
            .Add(AssetFolderPermissions.QueryAssetFolder)
            .Add(AssetFolderPermissions.ReadAssetFolder)
            .Add(AssetFolderPermissions.CreateAssetFolder)
            .Add(AssetFolderPermissions.UpdateAssetFolder)
            .Add(AssetFolderPermissions.DeleteAssetFolder)
            .Add(BackgroundTaskPermissions.ManageBackgroundTask)
            .Add(BackgroundTaskPermissions.QueryBackgroundTask)
            .Add(BackgroundTaskPermissions.CancelBackgroundTask)
            .Add(WebHookPermissions.ManageWebHook)
            .Add(WebHookPermissions.QueryWebHook)
            .Add(WebHookPermissions.ReadWebHook)
            .Add(WebHookPermissions.CreateWebHook)
            .Add(WebHookPermissions.UpdateWebHook)
            .Add(WebHookPermissions.DeleteWebHook);

        return manager;
    }
}
