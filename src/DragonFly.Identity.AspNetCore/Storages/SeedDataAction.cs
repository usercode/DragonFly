using DragonFly.Core.Permissions;
using DragonFly.Identity;
using DragonFly.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
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

        using (new DisablePermissions())
        {
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = "Administrators";

            api.Permission().Items.Traverse(x => roleAdmin.Permissions.Add(x.Permission.Name));

            await api.Identity().CreateRoleAsync(roleAdmin);

            IdentityUser admin = new IdentityUser();
            admin.Username = Options.InitialUsername;
            admin.Roles.Add(roleAdmin);

            await api.Identity().CreateUserAsync(admin, Options.InitialPassword);
        }
    }                  
}
