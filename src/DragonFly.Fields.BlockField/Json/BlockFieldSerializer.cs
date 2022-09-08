using DragonFly.Fields.BlockField.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// BlockFieldSerializer
/// </summary>
public class BlockFieldSerializer
{
    static BlockFieldSerializer()
    {
        Options = new JsonSerializerOptions();
        Options.Converters.Add(new BlockFieldConverter());
        Options.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Options
    /// </summary>
    private static JsonSerializerOptions Options { get; }

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

        return result;
    }

    public static async Task<Document> DeserializeAsync(string? input)
    {
        if (input == null)
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

        string json = Encoding.UTF8.GetString(mem.ToArray());

        return await JsonSerializer.DeserializeAsync<Document>(mem, Options);
    }
}
