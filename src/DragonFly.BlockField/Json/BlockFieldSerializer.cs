// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DragonFly.BlockField.Json;

namespace DragonFly.BlockField;

/// <summary>
/// BlockFieldSerializer
/// </summary>
public static class BlockFieldSerializer
{
    static BlockFieldSerializer()
    {
        Options = new JsonSerializerOptions();
        Options.Converters.Add(new BlockFieldConverter());
        Options.Converters.Add(new JsonStringEnumConverter());

        //JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions() { TypeDiscriminatorPropertyName = "Type" };

        //foreach (Type blockFieldType in BlockFieldManager.Default.GetAllBlockTypes())
        //{
        //    optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(blockFieldType, blockFieldType.Name));
        //}

        //Options.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        //{
        //    Modifiers =
        //            {
        //                (JsonTypeInfo typeInfo) =>
        //                {
        //                    if (typeInfo.Type == typeof(Block))
        //                    {
        //                        typeInfo.PolymorphismOptions = optionsDerivedTypes;
        //                    }
        //                }
        //            }
        //};
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
