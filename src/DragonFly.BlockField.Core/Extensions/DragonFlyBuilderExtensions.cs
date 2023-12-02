// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.BlockField;
using DragonFly.Builders;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    public static TDragonFlyBuilder AddBlockFieldSerializerResolver<TDragonFlyBuilder>(this TDragonFlyBuilder builder, IJsonTypeInfoResolver resolver)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        BlockFieldSerializer.Options.TypeInfoResolverChain.Add(resolver);

        return builder;
    }
}
