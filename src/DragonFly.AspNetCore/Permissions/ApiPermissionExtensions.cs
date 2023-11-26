// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DragonFly;

public static class ApiPermissionExtensions
{
    /// <summary>
    /// Gets the permission manager.
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public static PermissionManager Permission(this IDragonFlyApi api)
    {
        return PermissionManager.Default;
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

    /// <summary>
    /// Authorizes access to a content item.
    /// </summary>
    public static async Task<bool> AuthorizeContentAsync(this IDragonFlyApi api, string schema, ContentAction action)
    {
        Permission permission = ContentPermissions.Create(schema, action);

        return await api.AuthorizeAsync(permission);
    }

    /// <summary>
    /// Authorizes a permission.
    /// </summary>
    public static async Task<bool> AuthorizeAsync(this IDragonFlyApi api, Permission permission)
    {
        ClaimsPrincipal? principal = PermissionPrincipal.GetCurrent();

        if (principal == null)
        {
            return false;
        }

        IAuthorizationService authorizationService = api.ServiceProvider.GetRequiredService<IAuthorizationService>();

        AuthorizationResult result = await authorizationService.AuthorizeAsync(principal, permission.ToAuthorizationPolicy());

        return result.Succeeded;
    }
}
