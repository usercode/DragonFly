using DragonFly.AspNetCore.Identity.Middlewares.Roles;
using DragonFly.AspNetCore.Identity.Middlewares.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.Middlewares
{
    internal static class Extensions
    {
        public static void UseIdentityApi(this IApplicationBuilder builder)
        {
            builder.UseUserApi();
            builder.UseRoleApi();
        }
    }
}
