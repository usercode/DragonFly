// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyAppExtensions
{
    public static DragonFlyApp App(this IDragonFlyApi api)
    {
        return api.ServiceProvider.GetRequiredService<DragonFlyApp>();
    }
}
