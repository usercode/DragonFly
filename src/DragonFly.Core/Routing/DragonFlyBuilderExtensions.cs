// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;
using DragonFly.Builders;

namespace DragonFly.Client;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds razor routing from the current assembly.
    /// </summary>
    public static TBuilder AddRazorRouting<TBuilder>(this TBuilder builder)
        where TBuilder : IDragonFlyBuilder
    {
        RazorRoutingManager.Default.Items.Add(Assembly.GetCallingAssembly());

        return builder;
    }
}
