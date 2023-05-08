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

    public static readonly Permission ManageUser = new Permission(UserGroup, "User:*", "Manage user");

    //user
    public static readonly Permission QueryUser = new Permission(UserGroup, "User:Query", "Query user", ManageUser);
    public static readonly Permission ReadUser = new Permission(UserGroup, "User:Read", "Read user", ManageUser, QueryUser);
    public static readonly Permission CreateUser = new Permission(UserGroup, "User:Create", "Create user", ManageUser);
    public static readonly Permission UpdateUser = new Permission(UserGroup, "User:Update", "Update user", ManageUser);
    public static readonly Permission DeleteUser = new Permission(UserGroup, "User:Delete", "Delete user", ManageUser);

    public static readonly Permission ChangePassword = new Permission(UserGroup, "User:ChangePassword", "Change user", ManageUser);

    //roles
    public static readonly PermissionGroup RoleGroup = new PermissionGroup("Roles");

    public static readonly Permission ManageRole = new Permission(RoleGroup, "Role:*", "Manage role");

    public static readonly Permission QueryRole = new Permission(RoleGroup, "Role:Query", "Query role", ManageRole);
    public static readonly Permission ReadRole = new Permission(RoleGroup, "Role:Read", "Read role", ManageRole, QueryRole);
    public static readonly Permission CreateRole = new Permission(RoleGroup, "Role:Create", "Create role", ManageRole);
    public static readonly Permission UpdateRole = new Permission(RoleGroup, "Role:Update", "Update role", ManageRole);
    public static readonly Permission DeleteRole = new Permission(RoleGroup, "Role:Delete", "Delete role", ManageRole);
}
