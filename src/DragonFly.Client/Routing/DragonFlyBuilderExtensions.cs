// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using System.Reflection;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddRazorRouting(this IDragonFlyBuilder builder)
    {
        RazorRoutingManager.Default.Items.Add(Assembly.GetCallingAssembly());

        return builder;
    }
}
