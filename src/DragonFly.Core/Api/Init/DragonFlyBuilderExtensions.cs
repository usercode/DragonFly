// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using DragonFly.Init;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    public static TBuilder Init<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action)
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IInitialize>(x => new InitializerItem(action));

        return builder;
    }

    public static IDragonFlyBuilder Init<T>(this IDragonFlyBuilder builder)
        where T : class, IInitialize
    {
        builder.Services.AddTransient<IInitialize, T>();

        return builder;
    }

    public static TBuilder PreInit<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action)
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IPreInitialize>(x => new InitializerItem(action));

        return builder;
    }

    public static IDragonFlyBuilder PreInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPreInitialize
    {
        builder.Services.AddTransient<IPreInitialize, T>();

        return builder;
    }

    public static TBuilder PostInit<TBuilder>(this TBuilder builder, Action<IDragonFlyApi> action)
        where TBuilder : IDragonFlyBuilder
    {
        builder.Services.AddTransient<IPostInitialize>(x => new InitializerItem(action));

        return builder;
    }

    public static IDragonFlyBuilder PostInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPostInitialize
    {
        builder.Services.AddTransient<IPostInitialize, T>();

        return builder;
    }        
}
