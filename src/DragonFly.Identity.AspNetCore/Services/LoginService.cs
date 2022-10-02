// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.AspNetCore.Services;

class LoginService : ILoginService
{
    public LoginService(
        MongoIdentityStore store,
        IPasswordHashGenerator passwordHashGenerator,
        IHttpContextAccessor httpcontextaccessor)
    {
        Store = store;
        PasswordHashGenerator = passwordHashGenerator;
        HttpContextAccessor = httpcontextaccessor;
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

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("UserId", user.Id.ToString()));
        claims.Add(new Claim("Username", user.Username));

        ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Password"));

        await HttpContextAccessor.HttpContext!.SignInAsync(principal, new AuthenticationProperties() { IsPersistent = isPersistent });

        return true;
    }

    public async Task Logout()
    {
        await HttpContextAccessor.HttpContext!.SignOutAsync();
    }
}
