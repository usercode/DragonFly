// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.Permissions;

/// <summary>
/// IPermissionAccessService
/// </summary>
public interface IPermissionAccessService
{
    Task<bool> CanAccessAsync(ClaimsPrincipal principal, string permission);
}
