// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.IO.Compression;
using System.Text.Json;
using DragonFly.Json;

namespace DragonFly;

/// <summary>
/// BlockFieldSerializerV0
/// </summary>
internal static class BlockFieldSerializerV0
{
    static BlockFieldSerializerV0()
    {
        Options = new JsonSerializerOptions();
        Options.Converters.Add(new BlockFieldConverter());
        Options.Converters.Add(new EnumStringJsonConverterFactory());
    }

    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options { get; }

    public static async Task<Document> DeserializeAsync(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new Document();
        }      

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
