using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.ContentItems;
using DragonFly.Core.Permissions;
using DragonFly.Core.Security;
using DragonFly.Identity;
using DragonFly.Identity.Permissions;
using DragonFly.Identity.Services;
using DragonFly.MongoDB.Options;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB;

/// <summary>
/// SeedDataAction
/// </summary>
class SeedDataAction : IPostInitialize
{
    public SeedDataAction(
        IOptions<MongoDbOptions> options,
        MongoIdentityStore mongoStore)
    {
        Options = options.Value;
        Store = mongoStore;
    }

    public MongoDbOptions Options { get; }

    /// <summary>
    /// Store
    /// </summary>
    private MongoIdentityStore Store { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        if (string.IsNullOrEmpty(Options.InitialUsername)
            || string.IsNullOrEmpty(Options.InitialPassword))
        {
            return;
        }

        if (Store.Users.AsQueryable().Any())
        {
            return;
        }

        using (new DisablePermissionState())
        {
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = "Administrators";

            api.Permission().Items.Traverse(x => roleAdmin.Permissions.Add(x.Permission.Name));

            await api.Identity().CreateRoleAsync(roleAdmin);

            IdentityUser admin = new IdentityUser();
            admin.Username = Options.InitialUsername;
            admin.Email = DefaultSecurity.DefaultEmail;
            admin.Roles.Add(roleAdmin);

            await api.Identity().CreateUserAsync(admin, Options.InitialPassword);
        }
    }                  
}
