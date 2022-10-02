// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public static class DragonFlyApiBuilder
{
    public static IIdentityService Identity(this IDragonFlyApi api)
    {
        IIdentityService service = api.ServiceProvider.GetRequiredService<IIdentityService>();

        return service;
    }
}
