using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Core.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
        public SeedDataAction(UserManager<DbUser> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// UserManager
        /// </summary>
        private UserManager<DbUser> UserManager { get; }

        public async Task ExecuteAsync(IDragonFlyApi api)
        {
            if (UserManager.Users.Any() == false)
            {
                DbUser admin = new DbUser();
                admin.UserName = DefaultSecurity.DefaultUsername;
                admin.Email = DefaultSecurity.DefaultEmail;

                var result = await UserManager.CreateAsync(admin, DefaultSecurity.DefaultPassword);

                if (result.Succeeded == false)
                {
                    throw new Exception("Could not create default admin user.");
                }
                
            }
        }
    }
}
