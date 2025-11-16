// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyApiBuilder
{
    extension(IDragonFlyApi api)
    {
        public IIdentityService Identity => api.ServiceProvider.GetRequiredService<IIdentityService>();
    }   
}
