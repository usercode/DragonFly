// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Identity.MongoDB;
using DragonFLy.ApiKeys;
using DragonFLy.ApiKeys.AspNetCore.Middlewares;
using DragonFLy.ApiKeys.AspNetCore.Services;
using DragonFLy.ApiKeys.Permissions;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.ApiKeys.Handlers;
using Microsoft.AspNetCore.Authorization;
using DragonFly.ApiKeys;
using DragonFly.AspNetCore.Builders;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddApiKeys(this IDragonFlyBuilder builder)
    {
        builder.Services.AddTransient<IApiKeyService, ApiKeyService>();

        builder.Services.AddSingleton<MongoIdentityStore>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        builder.Authentication.AddApiKey(ApiKeyAuthenticationDefaults.AuthenticationScheme);

        builder.AddPermissionScheme(ApiKeyAuthenticationDefaults.AuthenticationScheme);
        builder.AddPermissions(
                                ApiKeyPermissions.ManageApiKey, 
                                ApiKeyPermissions.QueryApiKey, 
                                ApiKeyPermissions.ReadApiKey, 
                                ApiKeyPermissions.CreateApiKey, 
                                ApiKeyPermissions.UpdateApiKey, 
                                ApiKeyPermissions.DeleteApiKey);

        return builder;
    }

    public static IDragonFlyMiddlewareBuilder MapApiKey(this IDragonFlyMiddlewareBuilder builder)
    {
        builder.Endpoints(x => x.MapApiKeyApi());

        return builder;
    }
}
