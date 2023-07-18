// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNetCore.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.AspNetCore;

public static class BuilderPermissionExtensions
{
    /// <summary>
    /// Adds permission scheme.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="scheme"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddPermissionScheme(this IDragonFlyBuilder builder, string scheme)
    {
        PermissionSchemeManager.Add(scheme);

        return builder;
    }
}
