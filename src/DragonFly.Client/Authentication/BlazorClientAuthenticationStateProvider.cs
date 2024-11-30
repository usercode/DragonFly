// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace DragonFly.AspNetCore.Permissions;

public class BlazorClientAuthenticationStateProvider : AuthenticationStateProvider, IPrincipalContext
{
    private ClaimsPrincipal Current { get; set; } = new ClaimsPrincipal();

    ClaimsPrincipal IPrincipalContext.Current { get => Current; set => Current = value; }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(Current));
    }

    public void SetCurrentPrincipal(ClaimsPrincipal currentPrincipal)
    {
        Current = currentPrincipal;

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentPrincipal)));
    }
}
