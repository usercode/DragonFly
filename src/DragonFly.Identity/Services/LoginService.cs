// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.AspNetCore.Identity.MongoDB.Storages.Models;
using DragonFly.Identity.Services;
using DragonFly.Security;
using Microsoft.AspNetCore.Authentication;
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
        IHttpContextAccessor httpcontextaccessor)
    {
        Store = store;
        PasswordHashGenerator = passwordHashGenerator;
        HttpContextAccessor = httpcontextaccessor;
        PrincipalContext = principalContext;
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

    public async Task<LoginResult> LoginAsync(string username, string password, bool isPersistent)
    {
        MongoIdentityUser? user = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Username == username).ConfigureAwait(false);

        if (user == null)
        {
            return new LoginResult(false);
        }

        string hashed = Convert.ToBase64String(PasswordHashGenerator.Generate(user.Username, Convert.FromBase64String(user.Salt), password));

        if (user.Password != hashed)
        {
            return new LoginResult(false);
        }

        IEnumerable<Claim> claims =
                                    [
                                        new Claim(ClaimTypes.Name, user.Username),
                                        new Claim("Name", $"user:{user.Username}"),
                                        new Claim("UserId", $"{user.Id}"),
                                        new Claim("Username", user.Username)
                                    ];

        ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Password"));

        await HttpContextAccessor.HttpContext!.SignInAsync(IdentityAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }).ConfigureAwait(false);
        
        return new LoginResult(true) { Username = user.Username, Claims = claims.Select(x=> new ClaimItem(x.Type, x.Value)).ToList() };
    }

    public async Task Logout()
    {
        await HttpContextAccessor.HttpContext!.SignOutAsync(IdentityAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<IdentityUser?> GetCurrentUserAsync()
    {
        if (PrincipalContext.Current is ClaimsPrincipal principal)
        {
            string? claimUserId = principal.FindFirstValue("UserId");

            if (claimUserId != null)
            {
                Guid userIdGuid = Guid.Parse(claimUserId);

                MongoIdentityUser? currentUser = await Store.Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userIdGuid).ConfigureAwait(false);

                if (currentUser != null)
                {
                    return currentUser.ToModel(Store);
                }
            }
        }

        return null;
    }
}
