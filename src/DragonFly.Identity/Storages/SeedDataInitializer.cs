// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.Init;
using DragonFly.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DragonFly.Identity;

/// <summary>
/// SeedDataInitializer
/// </summary>
class SeedDataInitializer : IPostInitialize
{
    public SeedDataInitializer(
        IOptions<MongoDbOptions> options,
        MongoIdentityStore mongoStore)
    {
        Options = options.Value;
        Store = mongoStore;
    }

    /// <summary>
    /// Options
    /// </summary>
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

        IdentityRole roleAdmin = new IdentityRole();
        roleAdmin.Name = "Administrators";

        roleAdmin.Permissions = api.Permissions.GetAll().Select(x => x.Name).ToList();

        await api.Identity.CreateRoleAsync(roleAdmin);

        IdentityUser admin = new IdentityUser();
        admin.Username = Options.InitialUsername;
        admin.Roles.Add(roleAdmin);

        await api.Identity.CreateUserAsync(admin, Options.InitialPassword);
    }                  
}
