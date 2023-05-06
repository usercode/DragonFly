// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class AssetFolderPermissions
{
    public static readonly PermissionGroup AssetFolderGroup = new PermissionGroup("Asset folders");

    public static readonly Permission ManageAssetFolder = new Permission(AssetFolderGroup, "ManageAssetFolder", "Manage asset folder");

    public static readonly Permission QueryAssetFolder = new Permission(AssetFolderGroup, "QueryAssetFolder", "Query asset folder", ManageAssetFolder);
    public static readonly Permission ReadAssetFolder = new Permission(AssetFolderGroup, "ReadAssetFolder", "Read asset folder", ManageAssetFolder, QueryAssetFolder);
    public static readonly Permission CreateAssetFolder = new Permission(AssetFolderGroup, "CreateAssetFolder", "Create asset folder", ManageAssetFolder);
    public static readonly Permission UpdateAssetFolder = new Permission(AssetFolderGroup, "UpdateAssetFolder", "Update asset folder", ManageAssetFolder);
    public static readonly Permission DeleteAssetFolder = new Permission(AssetFolderGroup, "DeleteAssetFolder", "Delete asset folder", ManageAssetFolder);
}
