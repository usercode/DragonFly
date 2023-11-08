﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.BlockField;
using DragonFly.Builders;

namespace DragonFly;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddJsonBlockTypeInfoResolver(this IDragonFlyBuilder builder, IJsonTypeInfoResolver resolver)
    {
        BlockFieldSerializer.Options.TypeInfoResolverChain.Add(resolver);

        return builder;
    }
}