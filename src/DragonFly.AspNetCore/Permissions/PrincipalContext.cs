// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore.Permissions;

public class PrincipalContext : IPrincipalContext
{
    private static readonly AsyncLocal<ClaimsPrincipal?> _current = new();

    public ClaimsPrincipal? Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }
}
