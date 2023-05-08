// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Authorization;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionRequirement
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    /// <summary>
    /// Permission
    /// </summary>
    public string Permission { get; set; }
}
