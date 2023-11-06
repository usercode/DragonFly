﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using DragonFly.BlockField.Core.Json;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldSerializer
/// </summary>
public static class BlockFieldSerializer
{
    static BlockFieldSerializer()
    {
        Options = new JsonSerializerOptions() { TypeInfoResolver = BlockFieldSerializerResolver.Default };
        Options.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Options
    /// </summary>
    private static JsonSerializerOptions Options { get; }

    public static async Task<byte[]> SerializeBlockAsync(IEnumerable<Block> blocks)
    {
        MemoryStream jsonStream = new MemoryStream();

        await JsonSerializer.SerializeAsync(jsonStream, blocks, Options);

        return jsonStream.ToArray();
    }

    public static async Task<Block[]> DeserializeBlockAsync(byte[] buffer)
    {
        Block[]? blocks = await JsonSerializer.DeserializeAsync<Block[]>(new MemoryStream(buffer), Options);

        if (blocks == null)
        {
            return Array.Empty<Block>();
        }

        return blocks;
    }

    public static Task<string?> SerializeAsync(Document document)
    {
        return BlockFieldSerializerV1.SerializeAsync(document);
    }

    public static async Task<Document> DeserializeAsync(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new Document();
        }

        if (input.StartsWith("V1_"))
        {
            return await BlockFieldSerializerV1.DeserializeAsync(input);
        }
        else
        {
            return await BlockFieldSerializerV0.DeserializeAsync(input);
        }
    }
}
