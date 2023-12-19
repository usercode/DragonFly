// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;
using DragonFly.Client.Builders;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds razor routing from the current assembly.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddRazorRouting(this IDragonFlyBuilder builder)
    {
        RazorRoutingManager.Default.Items.Add(Assembly.GetCallingAssembly());

        return builder;
    }
}
