using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.Core.Security;
using DragonFly.Identity;
using DragonFly.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
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
            MongoIdentityStore mongoStore,
            IIdentityService userService)
        {
            Store = mongoStore;
            UserManager = userService;
        }

        /// <summary>
        /// UserManager
        /// </summary>
        private IIdentityService UserManager { get; }

        /// <summary>
        /// Store
        /// </summary>
        private MongoIdentityStore Store { get; }

        public async Task ExecuteAsync(IDragonFlyApi api)
        {
            if (Store.Users.AsQueryable().Any() == false)
            {
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = "Administrators";

                await UserManager.CreateRoleAsync(roleAdmin);

                IdentityRole roleReaders = new IdentityRole();
                roleReaders.Name = "ReadOnly";

                await UserManager.CreateRoleAsync(roleReaders);

                IdentityUser admin = new IdentityUser();
                admin.UserName = DefaultSecurity.DefaultUsername;
                admin.Email = DefaultSecurity.DefaultEmail;
                admin.Roles.Add(roleAdmin);

                await UserManager.CreateUserAsync(admin, DefaultSecurity.DefaultPassword);
               
            }
        }
    }
}
