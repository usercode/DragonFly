// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity.Permissions;

/// <summary>
/// IdentityPermissions
/// </summary>
public class IdentityPermissions
{
    //user
    public const string UserRead = "UserRead";
    public const string UserQuery = "UserQuery";
    public const string UserCreate = "UserCreate";
    public const string UserUpdate = "UserUpdate";
    public const string UserDelete = "UserDelete";

    public const string PasswordChange = "PasswordChange";

    //role
    public const string RoleRead = "RoleRead";
    public const string RoleQuery = "RoleQuery";
    public const string RoleCreate = "RoleCreate";
    public const string RoleUpdate = "RoleUpdate";
    public const string RoleDelete = "RoleDelete";

}
