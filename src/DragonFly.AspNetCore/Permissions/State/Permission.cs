// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly;

/// <summary>
/// Permission
/// </summary>
public static class Permission
{
    private static readonly AsyncLocal<ClaimsPrincipal?> Principal = new AsyncLocal<ClaimsPrincipal?>();
    public static ClaimsPrincipal? GetCurrentPrincipal() => Principal.Value;
    public static void SetCurrentPrincipal(ClaimsPrincipal? principal) => Principal.Value = principal;
}
