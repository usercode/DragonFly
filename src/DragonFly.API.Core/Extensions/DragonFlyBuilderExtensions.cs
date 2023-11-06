// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.API;
using DragonFly.Builders;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddJsonTypeInfoResolver(this IDragonFlyBuilder builder, IJsonTypeInfoResolver resolver)
    {
        ApiJsonSerializerDefault.Options.TypeInfoResolverChain.Add(resolver);

        return builder;
    }
}
