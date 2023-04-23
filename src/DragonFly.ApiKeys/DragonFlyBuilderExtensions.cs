// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Builders;
using DragonFly.Permissions;
using DragonFLy.ApiKeys;
using DragonFLy.ApiKeys.AspNetCore.Middlewares;
using DragonFLy.ApiKeys.AspNetCore.Services;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton<MongoIdentityStore>();

        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();
        builder.Services.AddTransient<IPermissionAuthorizationService, PermissionAuthorizationService>();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Init(api =>
        {
            api.Permission()
                            .Add("ApiKey", x => x
                                            .Add(ApiKeyPermissions.ApiKeyRead, description: "Read apikey", sortkey: 0, childs: x => x
                                                    .Add(ApiKeyPermissions.ApiKeyQuery, description: "Query apikey"))
                                            .Add(ApiKeyPermissions.ApiKeyCreate, description: "Create apikey", sortkey: 1)
                                            .Add(ApiKeyPermissions.ApiKeyUpdate, description: "Update apikey", sortkey: 2)
                                            .Add(ApiKeyPermissions.ApiKeyDelete, description: "Delete apikey", sortkey: 3)
                                            );
        });

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapApiKey(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.PreAuthBuilder(x => x.UseMiddleware<ApiKeyMiddleware>());
        builder.Endpoints(x => x.MapApiKeyApi());

        return builder;
    }
}
