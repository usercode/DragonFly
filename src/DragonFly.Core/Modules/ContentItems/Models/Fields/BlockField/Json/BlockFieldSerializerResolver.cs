﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly;

internal class BlockFieldSerializerResolver : DefaultJsonTypeInfoResolver
{
    public static BlockFieldSerializerResolver Default { get; } = new BlockFieldSerializerResolver();

    private BlockFieldSerializerResolver()
    {
    }

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        if (jsonTypeInfo.Type == typeof(Block))
        {
            JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions() { TypeDiscriminatorPropertyName = "Type" };

            foreach (BlockFactory blockFactory in BlockManager.Default.GetAllBlocks())
            {
                optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(blockFactory.BlockType, blockFactory.BlockName));
            }

            jsonTypeInfo.PolymorphismOptions = optionsDerivedTypes;
        }
        
        return jsonTypeInfo;
    }
}
