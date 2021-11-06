using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Services;
using DragonFly.Identity.Services;
using DragonFly.Core.Builders;
using DragonFly.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using DragonFly.AspNetCore.Identity.MongoDB.Services.Base;
using DragonFly.AspNet.Middleware;
using Microsoft.AspNetCore.Builder;
using DragonFly.AspNetCore.Identity.Middlewares;
using Microsoft.AspNetCore.Http;
using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.Permissions.AspNetCore;
using DragonFly.Identity.AspNetCore.Permissions;
using DragonFly.Identity.AspNetCore.Services;
using DragonFly.Identity.AspNetCore.Authorization;
using DragonFly.Identity.Permissions;
using DragonFly.Permissions;
using DragonFly.AspNetCore.Middleware;
using DragonFly.AspNetCore.API.Middlewares.Logins;

namespace DragonFly.Identity.AspNetCore.MongoDB
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddMongoDbIdentity(this IDragonFlyBuilder builder)
        {
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IIdentityService, IdentityService>();

            builder.Services.AddSingleton<IAuthorizePermissionService, PermissionAuthorizeService>();
            builder.Services.AddSingleton<IPasswordHashGenerator, PasswordHashGenerator>();

            builder.Services.AddSingleton<MongoIdentityStore>();

            builder.Services
                            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie();

            builder.Services.AddAuthorization();

            builder.Services.Decorate<IIdentityService, IdentityServiceAuthorization>();

            builder.Init(api =>
            {
                api.Permission()
                                .Add("User", x => x
                                                .Add(IdentityPermissions.UserRead, description: "Read user", sortkey: 0, childs: x => x
                                                        .Add(IdentityPermissions.UserQuery, description: "Query user"))
                                                .Add(IdentityPermissions.UserCreate, description: "Create user", sortkey: 1)
                                                .Add(IdentityPermissions.UserUpdate, description: "Update user", sortkey: 2)
                                                .Add(IdentityPermissions.UserDelete, description: "Delete user", sortkey: 3)
                                                )
                                .Add("Role", x => x
                                                .Add(IdentityPermissions.RoleRead, description: "Read role", sortkey: 0, childs: x => x
                                                        .Add(IdentityPermissions.RoleQuery, description: "Query role"))
                                                .Add(IdentityPermissions.RoleCreate, description: "Create role", sortkey: 1)
                                                .Add(IdentityPermissions.RoleUpdate, description: "Update role", sortkey: 2)
                                                .Add(IdentityPermissions.RoleDelete, description: "Delete role", sortkey: 3)
                                );
            });

            builder.PostInit<SeedDataAction>();

            return builder;
        }

        public static IDragonFlyFullBuilder MapIdentity(this IDragonFlyFullBuilder builder)
        {
            builder.PreAuthBuilder(x => x.UseMiddleware<LoginMiddleware>());
            builder.Endpoints(x => x.MapIdentityApi());

            return builder;
        }
    }
}
