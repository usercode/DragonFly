using DragonFly;
using DragonFly.AspNet.Middleware;
using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.Core.Builders;
using DragonFly.Permissions;
using DragonFly.Permissions.AspNetCore;
using DragonFLy.ApiKeys.AspNetCore.Middlewares;
using DragonFLy.ApiKeys.AspNetCore.Services;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFLy.ApiKeys
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
        {
            builder.Services.AddSingleton<MongoIdentityStore>();

            builder.Services.AddTransient<IApiKeyService, ApiKeyService>();
            //builder.Services.AddTransient<IAuthorizePermissionService, PermissionService>();

            builder.Init(api =>
            {
                api.Permission()
                                .Add("ApiKey", x => x
                                                .Add(ApiKeyPermissions.ApiKeyRead, description: "Read apikey", sortkey: 0)
                                                .Add(ApiKeyPermissions.ApiKeyCreate, description: "Create apikey", sortkey: 1)
                                                .Add(ApiKeyPermissions.ApiKeyUpdate, description: "Update apikey", sortkey: 2)
                                                .Add(ApiKeyPermissions.ApiKeyDelete, description: "Delete apikey", sortkey: 3)
                                                );
            });

            return builder;
        }

        public static IDragonFlyApplicationBuilder UseApiKey(this IDragonFlyApplicationBuilder builder)
        {
            builder.UseMiddleware<ApiKeyMiddleware>();
            builder.UseApiKeyApi();

            return builder;
        }
    }
}