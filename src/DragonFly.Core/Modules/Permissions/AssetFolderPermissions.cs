// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class AssetFolderPermissions
{
    public static readonly PermissionGroup AssetFolderGroup = new PermissionGroup("Asset folders");

    public static readonly Permission ManageAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:*", "Manage asset folder");

    public static readonly Permission QueryAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:Query", "Query asset folder", ManageAssetFolder);
    public static readonly Permission ReadAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:Read", "Read asset folder", ManageAssetFolder, QueryAssetFolder);
    public static readonly Permission CreateAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:Create", "Create asset folder", ManageAssetFolder);
    public static readonly Permission UpdateAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:Update", "Update asset folder", ManageAssetFolder);
    public static readonly Permission DeleteAssetFolder = new Permission(AssetFolderGroup, "AssetFolder:Delete", "Delete asset folder", ManageAssetFolder);
}
