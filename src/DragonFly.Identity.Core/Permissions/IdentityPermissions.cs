// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity.Permissions;

/// <summary>
/// IdentityPermissions
/// </summary>
public static class IdentityPermissions
{
    public static readonly PermissionGroup UserGroup = new PermissionGroup("Users");

    //user
    public static readonly Permission ReadUser = new Permission(UserGroup, "ReadUser", "Read user");
    public static readonly Permission QueryUser = new Permission(UserGroup, "QueryUser", "Query user");
    public static readonly Permission CreateUser = new Permission(UserGroup, "CreateUser", "Create user");
    public static readonly Permission UpdateUser = new Permission(UserGroup, "UpdateUser", "Update user");
    public static readonly Permission DeleteUser = new Permission(UserGroup, "DeleteUser", "Delete user");

    public static readonly Permission ChangePassword = new Permission(UserGroup, "ChangePassword", "Change user");

    public static readonly PermissionGroup RoleGroup = new PermissionGroup("Roles");

    //role
    public static readonly Permission ReadRole = new Permission(RoleGroup, "ReadRole", "Read role");
    public static readonly Permission QueryRole = new Permission(RoleGroup, "QueryRole", "Query role");
    public static readonly Permission CreateRole = new Permission(RoleGroup, "CreateRole", "Create role");
    public static readonly Permission UpdateRole = new Permission(RoleGroup, "UpdateRole", "Update role");
    public static readonly Permission DeleteRole = new Permission(RoleGroup, "DeleteRole", "Delete role");
}
