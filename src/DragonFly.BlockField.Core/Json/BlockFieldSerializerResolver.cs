// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly.BlockField.Core.Json;

internal class BlockFieldSerializerResolver : IJsonTypeInfoResolver
{
    public static BlockFieldSerializerResolver Default { get; } = new BlockFieldSerializerResolver();

    private BlockFieldSerializerResolver()
    {
    }

    public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo? jsonTypeInfo = ((IJsonTypeInfoResolver)BlockFieldJsonSerializerContext.Default).GetTypeInfo(type, options);

        if (jsonTypeInfo != null)
        {
            if (jsonTypeInfo.Type == typeof(Block))
            {
                JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions() { TypeDiscriminatorPropertyName = "Type" };

                foreach (BlockFactory blockFactory in BlockFieldManager.Default.GetAllBlocks())
                {
                    optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(blockFactory.BlockType, blockFactory.BlockType.Name));
                }

                jsonTypeInfo.PolymorphismOptions = optionsDerivedTypes;
            }                        
        }

        return jsonTypeInfo;
    }
}
