﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFly.AspNetCore.Builders;
using DragonFLy.ApiKeys;
using DragonFLy.ApiKeys.AspNetCore.Middlewares;
using DragonFLy.ApiKeys.AspNetCore.Services;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.ApiKeys.Handlers;
using Microsoft.AspNetCore.Authorization;
using DragonFly.ApiKeys;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.Services.AddSingleton<MongoIdentityStore>();

        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Services.AddAuthentication().AddApiKey(ApiKeyAuthenticationDefaults.AuthenticationScheme);
        builder.Services.AddAuthorization();

        builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        builder.Init(api =>
        {
            api.Permissions()
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
        builder.Endpoints(x => x.MapApiKeyApi());

        return builder;
    }
}
