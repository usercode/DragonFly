using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Identity.Middlewares;
using DragonFly.Core.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DragonFly.AspNetCore.Identity
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddIdentity<TUser, TRole>(this IDragonFlyBuilder builder, Action<IdentityBuilder> options)
            where TUser : class
            where TRole : class
        {
            builder.Services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                //Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                //User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.LoginPath = "/manager/login";
                options.AccessDeniedPath = "/manager/access-denied";
                options.SlidingExpiration = true;
            });

            IdentityBuilder identity = builder.Services.AddIdentity<TUser, TRole>()
                                                        .AddDefaultTokenProviders();

            options?.Invoke(identity);

            return builder;
        }

        public static IDragonFlyApplicationBuilder UseIdentity(this IDragonFlyApplicationBuilder builder)
        {
            builder.UseIdentityApi();

            return builder;
        }
    }
}