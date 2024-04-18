// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.AspNetCore.Identity.MongoDB.Storages.Models;
using DragonFly.Identity.Services;
using DragonFly.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Security.Claims;

namespace DragonFly.Identity.AspNetCore.Services;

class LoginService : ILoginService
{
    public LoginService(
        MongoIdentityStore store,
        IPasswordHashGenerator passwordHashGenerator,
        IPrincipalContext principalContext,
        IHttpContextAccessor httpcontextaccessor,
        AuthenticationStateProvider authenticationStateProvider)
    {
        Store = store;
        PasswordHashGenerator = passwordHashGenerator;
        HttpContextAccessor = httpcontextaccessor;
        PrincipalContext = principalContext;
        AuthenticationStateProvider = authenticationStateProvider;
    }

    /// <summary>
    /// Store
    /// </summary>
    public MongoIdentityStore Store { get; }

    /// <summary>
    /// PasswordGenerater
    /// </summary>
    public IPasswordHashGenerator PasswordHashGenerator { get; }

    /// <summary>
    /// HttpContextAccessor
    /// </summary>
    public IHttpContextAccessor HttpContextAccessor { get; }

    /// <summary>
    /// PrincipalContext
    /// </summary>
    public IPrincipalContext PrincipalContext { get; }

    /// <summary>
    /// AuthenticationStateProvider
    /// </summary>
    public AuthenticationStateProvider AuthenticationStateProvider { get; }

    public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
    {
        MongoIdentityUser user = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return false;
        }

        string hashed = Convert.ToBase64String(PasswordHashGenerator.Generate(user.Username, Convert.FromBase64String(user.Salt), password));

        if (user.Password != hashed)
        {
            return false;
        }

        IEnumerable<Claim> claims =
                                    [
                                        new Claim(ClaimTypes.Name, user.Username),
                                        new Claim("Name", $"user:{user.Username}"),
                                        new Claim("UserId", $"{user.Id}"),
                                        new Claim("Username", user.Username)
                                    ];

        ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Password"));

        PrincipalContext.Principal = principal;

        if (AuthenticationStateProvider is ServerAuthenticationStateProvider serverAuthenticationStateProvider)
        {
            serverAuthenticationStateProvider.SetAuthenticationState(Task.FromResult(new AuthenticationState(principal)));
        }

        if (HttpContextAccessor.HttpContext?.WebSockets.IsWebSocketRequest == false)
        {
            await HttpContextAccessor.HttpContext.SignInAsync(IdentityAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent });
        }        

        return true;
    }

    public async Task Logout()
    {
        if (HttpContextAccessor.HttpContext?.WebSockets.IsWebSocketRequest == false)
        {
            await HttpContextAccessor.HttpContext!.SignOutAsync(IdentityAuthenticationDefaults.AuthenticationScheme);
        }
    }

    public async Task<IdentityUser?> GetCurrentUserAsync()
    {
        if (PrincipalContext.Principal is ClaimsPrincipal principal)
        {
            string? claimUserId = principal.FindFirstValue("UserId");

            if (claimUserId != null)
            {
                Guid userIdGuid = Guid.Parse(claimUserId);

                MongoIdentityUser? currentUser = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userIdGuid);

                if (currentUser != null)
                {
                    return currentUser.ToModel(Store);
                }
            }
        }

        return null;
    }
}
