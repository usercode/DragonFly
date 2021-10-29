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
using DragonFly.AspNetCore.API.Middlewares.Logins;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddIdentityMongoDb(this IDragonFlyBuilder builder)
        {
            return AddIdentityMongoDb(builder, x => { });
        }

        public static IDragonFlyBuilder AddIdentityMongoDb(this IDragonFlyBuilder builder, Action<MongoDbIdentityOptions> options)
        {
            builder.Services.Configure(options);

            builder.Services.AddTransient<ILoginService, IdentityService>();
            builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddTransient<IPasswordHashGenerator, PasswordHashGenerator>();

            builder.Services.AddSingleton<MongoIdentityStore>();

            builder.Services
                            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie();

            builder.Services.AddAuthorization();

            builder.PostInit<SeedDataAction>();

            return builder;
        }

        public static IDragonFlyApplicationBuilder UseIdentity(this IDragonFlyApplicationBuilder builder)
        {
            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseMiddleware<LoginMiddleware>();
            builder.UseMiddleware<InternalApiKeyMiddleware>();
            builder.UseMiddleware<RequireAuthentificationMiddleware>();

            builder.Map("/identity",
                x =>
                {
                    x.UseIdentityApi();
                });

            return builder;
        }
    }
}
