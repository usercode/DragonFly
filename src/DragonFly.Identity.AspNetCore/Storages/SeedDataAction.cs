using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.Core.Security;
using DragonFly.Identity;
using DragonFly.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    /// <summary>
    /// SeedDataAction
    /// </summary>
    class SeedDataAction : IPostInitialize
    {
        public SeedDataAction(
            IOptions<MongoDbIdentityOptions> options,
            MongoIdentityStore mongoStore)
        {
            Options = options.Value;

            Store = mongoStore;
        }

        public MongoDbIdentityOptions Options { get; }

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

            await api.Identity().CreateRoleAsync(roleAdmin);

            IdentityRole roleReaders = new IdentityRole();
            roleReaders.Name = "ReadOnly";

            await api.Identity().CreateRoleAsync(roleReaders);

            IdentityUser admin = new IdentityUser();
            admin.UserName = Options.InitialUsername;
            admin.Email = DefaultSecurity.DefaultEmail;
            admin.Roles.Add(roleAdmin);

            await api.Identity().CreateUserAsync(admin, Options.InitialPassword);
        }                  
    }
}
