// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyApiBuilder
{
    public static IIdentityService Identity(this IDragonFlyApi api)
    {
        IIdentityService service = api.ServiceProvider.GetRequiredService<IIdentityService>();

        return service;
    }
}
