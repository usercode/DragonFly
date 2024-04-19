// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore.Permissions;

public class PrincipalContext : IPrincipalContext
{
    public ClaimsPrincipal? Current
    {
        get => Principal.Current;
        set => Principal.Current = value;
    }
}
