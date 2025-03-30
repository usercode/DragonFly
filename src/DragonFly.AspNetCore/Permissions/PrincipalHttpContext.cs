// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore.Permissions;

public class PrincipalHttpContext : IPrincipalContext
{
    public PrincipalHttpContext(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    private readonly IHttpContextAccessor HttpContextAccessor;

    public ClaimsPrincipal? Current
    {
        get => HttpContextAccessor.HttpContext?.User;
    }
}
