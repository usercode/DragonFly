// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Runtime.CompilerServices;
using DragonFly.Builders;
using DragonFly.Init;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyInitBuilderExtensions
{
    public static TBuilder Init<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action, [CallerMemberName] string name = "")
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IInitialize>(x => new InitializerItem(name, action));

        return builder;
    }

    public static IDragonFlyBuilder Init<T>(this IDragonFlyBuilder builder)
        where T : class, IInitialize
    {
        builder.Services.AddTransient<IInitialize, T>();

        return builder;
    }

    public static TBuilder PreInit<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action, [CallerMemberName] string name = "")
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IPreInitialize>(x => new InitializerItem(name, action));

        return builder;
    }

    public static IDragonFlyBuilder PreInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPreInitialize
    {
        builder.Services.AddTransient<IPreInitialize, T>();

        return builder;
    }

    public static TBuilder PostInit<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action, [CallerMemberName] string name = "")
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IPostInitialize>(x => new InitializerItem(name, action));

        return builder;
    }

    public static IDragonFlyBuilder PostInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPostInitialize
    {
        builder.Services.AddTransient<IPostInitialize, T>();

        return builder;
    }        
}
