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
    private static readonly AsyncLocal<bool> Enabled = new AsyncLocal<bool>();
    private static readonly AsyncLocal<ClaimsPrincipal?> Principal = new AsyncLocal<ClaimsPrincipal?>();

    public static bool IsEnabled => Enabled.Value;
    public static void Enable() => Enabled.Value = true;
    public static void Disable() => Enabled.Value = false;
    public static ClaimsPrincipal? GetCurrentPrincipal() => Principal.Value;
    public static void SetCurrentPrincipal(ClaimsPrincipal? principal) => Principal.Value = principal;
    
    public static SuppressPermissions Suppress()
    {
        bool isEnabled = IsEnabled;

        Disable();

        return new SuppressPermissions(isEnabled);
    }

    public static async Task<T> SuppressAsync<T>(Func<Task<T>> func)
    {
        using (Suppress())
        {
            return await func();
        }
    }
}
