using DragonFly.Fields.BlockField.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Storage.Serializers;

/// <summary>
/// DocumentSerializer
/// </summary>
public class DocumentSerializer
{
    public DocumentSerializer()
    {
        Options = new JsonSerializerOptions();
        Options.Converters.Add(new BlockFieldConverter());
    }

    /// <summary>
    /// Options
    /// </summary>
    private JsonSerializerOptions Options { get; }

    public async Task<string> SerializeAsync(Document? document)
    {
        MemoryStream jsonStream = new MemoryStream();
        
        await JsonSerializer.SerializeAsync(jsonStream, document, Options);

        jsonStream.Seek(0, SeekOrigin.Begin);

        MemoryStream mem = new MemoryStream();
        using (GZipStream brotli = new GZipStream(mem, CompressionLevel.Optimal))
        {
            await jsonStream.CopyToAsync(brotli);
        }

        string result = Convert.ToBase64String(mem.ToArray());

        return result;
    }

    public async Task<Document?> DeserializeAsync(string? input)
    {
        if (input == null)
        {
            return new Document();
        }

        byte[] buffer = Convert.FromBase64String(input);

        MemoryStream mem = new MemoryStream();
        using (GZipStream brotli = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress))
        {
            await brotli.CopyToAsync(mem);
        }

        mem.Seek(0, SeekOrigin.Begin);

        return await JsonSerializer.DeserializeAsync<Document>(mem, Options);
    }
}
