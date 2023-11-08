// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using CurrentBlockSerializer = DragonFly.BlockField.BlockFieldSerializerV1;

using System.Text.Json;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldSerializer
/// </summary>
public static class BlockFieldSerializer
{
    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options => CurrentBlockSerializer.Options;

    public static Task<byte[]> SerializeBlockAsync(IEnumerable<Block> blocks)
    {
        return CurrentBlockSerializer.SerializeBlockAsync(blocks);
    }

    public static Task<Block[]> DeserializeBlockAsync(byte[] buffer)
    {
        return CurrentBlockSerializer.DeserializeBlockAsync(buffer);
    }

    public static Task<string?> SerializeAsync(Document document)
    {
        return CurrentBlockSerializer.SerializeAsync(document);
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
