// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class AssetPermissions
{
    public static readonly PermissionGroup AssetGroup = new PermissionGroup("Assets");

    public static readonly Permission ManageAsset = new Permission(AssetGroup, "ManageAsset", "Manage asset");

    public static readonly Permission QueryAsset = new Permission(AssetGroup, "QueryAsset", "Query asset", ManageAsset);
    public static readonly Permission ReadAsset = new Permission(AssetGroup, "ReadAsset", "Read asset", ManageAsset, QueryAsset);
    public static readonly Permission CreateAsset = new Permission(AssetGroup, "CreateAsset", "Create asset", ManageAsset);
    public static readonly Permission UpdateAsset = new Permission(AssetGroup, "UpdateAsset", "Update asset", ManageAsset);
    public static readonly Permission DeleteAsset = new Permission(AssetGroup, "DeleteAsset", "Delete asset", ManageAsset);
    public static readonly Permission PublishAsset = new Permission(AssetGroup, "PublishAsset", "Publish asset", ManageAsset);
    public static readonly Permission UnpublishAsset = new Permission(AssetGroup, "UnpublishAsset", "Unpublish asset", ManageAsset);
    public static readonly Permission UploadAsset = new Permission(AssetGroup, "UploadAsset", "Upload asset", ManageAsset);
    public static readonly Permission DownloadAsset = new Permission(AssetGroup, "DownloadAsset", "Download asset", ManageAsset);
}
