// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.ApiKeys;
using DragonFly.ApiKeys.AspNetCore.Middlewares;
using DragonFly.ApiKeys.AspNetCore.Services;
using DragonFly.ApiKeys.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using DragonFly.AspNetCore.Builders;
using Microsoft.AspNetCore.Builder;
using DragonFly.ApiKeys.Middlewares;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Services.AddSingleton<MongoIdentityStore>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        builder.Init(api =>
        {
            api.Permission()
                            .Add(ApiKeyPermissions.ManageApiKey)
                            .Add(ApiKeyPermissions.QueryApiKey)
                            .Add(ApiKeyPermissions.ReadApiKey)
                            .Add(ApiKeyPermissions.CreateApiKey)
                            .Add(ApiKeyPermissions.UpdateApiKey)
                            .Add(ApiKeyPermissions.DeleteApiKey);
        });

        builder.AddEndpoint(x => x.MapApiKeyApi());
        builder.UseApplicationBuilder(x => x.UseMiddleware<ApiKeysMiddleware>());

        return builder;
    }
}
