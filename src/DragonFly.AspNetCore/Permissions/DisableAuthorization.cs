// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;

namespace DragonFly.AspNetCore.Permissions;

public class DisableAuthorization : IDisposable
{
    public DisableAuthorization(IPrincipalContext context)
    {
        Context = context;
        OldPrincipal = Context.Current;

        Context.Current = null;
    }

    /// <summary>
    /// Context
    /// </summary>
    private IPrincipalContext Context { get; }

    /// <summary>
    /// OldPrincipal
    /// </summary>
    private ClaimsPrincipal? OldPrincipal { get; }

    public void Dispose()
    {
        Context.Current = OldPrincipal;
    }
}
