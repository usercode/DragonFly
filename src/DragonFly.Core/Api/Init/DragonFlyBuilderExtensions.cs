// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder Init(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
    {
        builder.Services.AddTransient<IInitialize>(x => new InitItem(action));

        return builder;
    }

    public static IDragonFlyBuilder Init<T>(this IDragonFlyBuilder builder)
        where T : class, IInitialize
    {
        builder.Services.AddTransient<IInitialize, T>();

        return builder;
    }

    public static IDragonFlyBuilder PreInit(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
    {
        builder.Services.AddTransient<IPreInitialize>(x => new InitItem(action));

        return builder;
    }

    public static IDragonFlyBuilder PreInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPreInitialize
    {
        builder.Services.AddTransient<IPreInitialize, T>();

        return builder;
    }

    public static IDragonFlyBuilder PostInit(this IDragonFlyBuilder builder, Action<IDragonFlyApi> action)
    {
        builder.Services.AddTransient<IPostInitialize>(x => new InitItem(action));

        return builder;
    }

    public static IDragonFlyBuilder PostInit<T>(this IDragonFlyBuilder builder)
        where T : class, IPostInitialize
    {
        builder.Services.AddTransient<IPostInitialize, T>();

        return builder;
    }        
}
