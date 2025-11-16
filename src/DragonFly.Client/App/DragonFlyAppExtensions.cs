// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyAppExtensions
{
    extension(IDragonFlyApi api)
    {
        public DragonFlyApp App => api.ServiceProvider.GetRequiredService<DragonFlyApp>();
    }    
}
