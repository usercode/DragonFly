// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore;

/// <summary>
/// Principal
/// </summary>
public static class Principal
{
    private static readonly AsyncLocal<ClaimsPrincipal?> _current = new AsyncLocal<ClaimsPrincipal?>();

    /// <summary>
    /// Gets the current principal.
    /// </summary>
    public static ClaimsPrincipal? Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}
