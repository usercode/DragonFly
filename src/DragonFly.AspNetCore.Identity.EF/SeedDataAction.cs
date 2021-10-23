using DragonFly.AspNetCore.Identity.EF;
using DragonFly.AspNetCore.Identity.EF.Models;
using DragonFly.Core.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity
{
    class SeedDataAction : IPostInitialize
    {
        public SeedDataAction(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private IServiceProvider ServiceProvider { get; }

        public async Task ExecuteAsync(IDragonFlyApi api)
        {
            var scope = ServiceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DragonFlyIdentityContext>();

            context.Database.EnsureCreated();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();

            DbUser admin = await userManager.FindByNameAsync(DefaultSecurity.DefaultUsername);

            if (admin == null)
            {
                admin = new DbUser();
                admin.Id = Guid.NewGuid();
                admin.UserName = DefaultSecurity.DefaultUsername;

                var r = await userManager.CreateAsync(admin, DefaultSecurity.DefaultPassword);

                
                
            }
        }
    }
}
