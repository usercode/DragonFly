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

    public static readonly Permission ManageUser = new Permission(UserGroup, "ManageUser", "Manage user");

    //user
    public static readonly Permission QueryUser = new Permission(UserGroup, "QueryUser", "Query user", ManageUser);
    public static readonly Permission ReadUser = new Permission(UserGroup, "ReadUser", "Read user", ManageUser, QueryUser);
    public static readonly Permission CreateUser = new Permission(UserGroup, "CreateUser", "Create user", ManageUser);
    public static readonly Permission UpdateUser = new Permission(UserGroup, "UpdateUser", "Update user", ManageUser);
    public static readonly Permission DeleteUser = new Permission(UserGroup, "DeleteUser", "Delete user", ManageUser);

    public static readonly Permission ChangePassword = new Permission(UserGroup, "ChangePassword", "Change user", ManageUser);

    //roles
    public static readonly PermissionGroup RoleGroup = new PermissionGroup("Roles");

    public static readonly Permission ManageRole = new Permission(RoleGroup, "ManageRole", "Manage role");

    public static readonly Permission QueryRole = new Permission(RoleGroup, "QueryRole", "Query role", ManageRole);
    public static readonly Permission ReadRole = new Permission(RoleGroup, "ReadRole", "Read role", ManageRole, QueryRole);
    public static readonly Permission CreateRole = new Permission(RoleGroup, "CreateRole", "Create role", ManageRole);
    public static readonly Permission UpdateRole = new Permission(RoleGroup, "UpdateRole", "Update role", ManageRole);
    public static readonly Permission DeleteRole = new Permission(RoleGroup, "DeleteRole", "Delete role", ManageRole);
}
