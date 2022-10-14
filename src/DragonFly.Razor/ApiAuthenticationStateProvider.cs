// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DragonFly.Client;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    public ApiAuthenticationStateProvider()
    {
        ClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public ClaimsPrincipal ClaimsPrincipal { get; set; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(ClaimsPrincipal);
    }

    public void MarkUserAsAuthenticated()
    {
        ClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { }, "password"));
        var authState = Task.FromResult(new AuthenticationState(ClaimsPrincipal));

        NotifyAuthenticationStateChanged(authState);
    }
}
