// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Globalization;
using UglyToad.PdfPig;

namespace DragonFly.AspNetCore;

/// <summary>
/// PdfAssetProcessing
/// </summary>
public class PdfAssetProcessing : IAssetProcessing
{
    public IEnumerable<string> SupportedMimetypes
    {
        get => new[] { MimeTypes.Pdf };
    }

    public async Task<bool> OnAssetChangedAsync(IAssetProcessingContext context)
    {
        using Stream stream = await context.OpenAssetStreamAsync();

        MemoryStream mem = new MemoryStream();
        await stream.CopyToAsync(mem);

        mem.Seek(0, SeekOrigin.Begin);

        PdfMetadata metadata = new PdfMetadata();

        using (PdfDocument document = PdfDocument.Open(mem))
        {
            metadata.CountPages = document.NumberOfPages;
            metadata.IsEncrypted = document.IsEncrypted;
            metadata.PdfVersion = document.Version.ToString(CultureInfo.InvariantCulture);
        }

        await context.SetMetadataAsync(metadata);

        return true;
    }
}
