// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public static class GeneralPermissions
{
    public static readonly PermissionGroup PermissionGroup = new PermissionGroup("Permissions");

    public static readonly Permission ReadPermissions = new Permission(PermissionGroup, "Permissions:Read", "Read permissions");
}
