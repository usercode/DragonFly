// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.API;
using DragonFly.Builders;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds a JsonSerializerContext to the REST api.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    public static TDragonFlyBuilder AddRestSerializerResolver<TDragonFlyBuilder>(this TDragonFlyBuilder builder, IJsonTypeInfoResolver resolver)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        ApiJsonSerializerDefault.Options.TypeInfoResolverChain.Add(resolver);

        return builder;
    }
}
