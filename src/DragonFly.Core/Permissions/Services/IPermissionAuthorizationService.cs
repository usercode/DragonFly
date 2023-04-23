// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using System.Threading.Tasks;

namespace DragonFly.Permissions;

/// <summary>
/// IAuthorizePermissionService
/// </summary>
public interface IPermissionAuthorizationService
{
    Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission);
}
