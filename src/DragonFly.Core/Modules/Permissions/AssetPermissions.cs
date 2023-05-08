// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class AssetPermissions
{
    public static readonly PermissionGroup AssetGroup = new PermissionGroup("Assets");

    public static readonly Permission ManageAsset = new Permission(AssetGroup, "Asset:*", "Manage asset");

    public static readonly Permission QueryAsset = new Permission(AssetGroup, "Asset:Query", "Query asset", ManageAsset);
    public static readonly Permission ReadAsset = new Permission(AssetGroup, "Asset:Read", "Read asset", ManageAsset, QueryAsset);
    public static readonly Permission CreateAsset = new Permission(AssetGroup, "Asset:Create", "Create asset", ManageAsset);
    public static readonly Permission UpdateAsset = new Permission(AssetGroup, "Asset:Update", "Update asset", ManageAsset);
    public static readonly Permission DeleteAsset = new Permission(AssetGroup, "Asset:Delete", "Delete asset", ManageAsset);
    public static readonly Permission PublishAsset = new Permission(AssetGroup, "Asset:Publish", "Publish asset", ManageAsset);
    public static readonly Permission UnpublishAsset = new Permission(AssetGroup, "Asset:Unpublish", "Unpublish asset", ManageAsset);
    public static readonly Permission UploadAsset = new Permission(AssetGroup, "Asset:Upload", "Upload asset", ManageAsset);
    public static readonly Permission DownloadAsset = new Permission(AssetGroup, "Asset:Download", "Download asset", ManageAsset);
}
