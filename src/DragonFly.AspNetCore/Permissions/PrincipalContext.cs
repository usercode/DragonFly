// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore.Permissions;

public class PrincipalContext : IPrincipalContext
{
    public ClaimsPrincipal? Principal
    {
        get => AspNetCore.Principal.Current;
        set => AspNetCore.Principal.Current = value;
    }
}
