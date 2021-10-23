using DragonFly.AspNetCore.Identity.EF.Models;
using DragonFly.Core.Builders;
using DragonFly.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.EF
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddIdentityEF(this IDragonFlyBuilder builder, Action<DbContextOptionsBuilder> dbOptions)
        {
            builder.Services.AddDbContext<DragonFlyIdentityContext>(dbOptions);
            builder.Services.AddTransient<ILoginService, LoginService>();

            builder.AddIdentity<DbUser, DbRole>(x => x.AddEntityFrameworkStores<DragonFlyIdentityContext>());

            builder.Services.AddTransient<IPostInitialize, SeedDataAction>();

            return builder;
        }
    }
}
