// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldSerializerV1
/// </summary>
internal static class BlockFieldSerializerV1
{
    static BlockFieldSerializerV1()
    {
        Options = new JsonSerializerOptions() { TypeInfoResolver = BlockFieldSerializerResolver.Default };
        Options.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options { get; }

    public static async Task<byte[]> SerializeBlockAsync(IEnumerable<Block> blocks)
    {
        MemoryStream jsonStream = new MemoryStream();

        await JsonSerializer.SerializeAsync(jsonStream, blocks, Options);

        return jsonStream.ToArray();
    }

    public static async Task<IEnumerable<Block>> DeserializeBlockAsync(byte[] buffer)
    {
        IEnumerable<Block>? blocks = await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(new MemoryStream(buffer), Options);

        if (blocks == null)
        {
            return Enumerable.Empty<Block>();
        }

        return blocks;
    }

    public static async Task<string?> SerializeAsync(Document document)
    {
        if (document.Blocks.Count == 0)
        {
            return null;
        }

        MemoryStream jsonStream = new MemoryStream();

        await JsonSerializer.SerializeAsync(jsonStream, document, Options);

        jsonStream.Seek(0, SeekOrigin.Begin);

        MemoryStream mem = new MemoryStream();
        using (GZipStream zipStream = new GZipStream(mem, CompressionLevel.Optimal))
        {
            await jsonStream.CopyToAsync(zipStream);
        }

        string result = Convert.ToBase64String(mem.ToArray());

        //add version prefix
        return $"V1_{result}";
    }

    public static async Task<Document> DeserializeAsync(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new Document();
        }

        //remove version prefix
        input = input["V1_".Length..];

        byte[] buffer = Convert.FromBase64String(input);

        MemoryStream mem = new MemoryStream();
        using (GZipStream zipStream = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress))
        {
            await zipStream.CopyToAsync(mem);
        }

        mem.Seek(0, SeekOrigin.Begin);

        Document? result = await JsonSerializer.DeserializeAsync<Document>(mem, Options);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }
}
