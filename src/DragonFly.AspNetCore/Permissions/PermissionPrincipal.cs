// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore;

/// <summary>
/// PermissionPrincipal
/// </summary>
public static class PermissionPrincipal
{
    private static readonly AsyncLocal<ClaimsPrincipal?> Principal = new AsyncLocal<ClaimsPrincipal?>();

    /// <summary>
    /// Gets the current principal.
    /// </summary>
    /// <returns></returns>
    public static ClaimsPrincipal? GetCurrent() => Principal.Value;

    /// <summary>
    /// Sets the current principal.
    /// </summary>
    /// <param name="principal"></param>
    public static void SetCurrent(ClaimsPrincipal? principal) => Principal.Value = principal;
}
