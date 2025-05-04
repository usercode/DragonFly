// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Globalization;
using UglyToad.PdfPig;

namespace DragonFly.AspNetCore;

/// <summary>
/// PdfProcessing
/// </summary>
public class PdfProcessing : IAssetProcessing
{
    public bool CanUse(string mimeType)
    {
        return mimeType == MimeTypes.Pdf;
    }

    public async Task<bool> OnAssetChangedAsync(IAssetProcessingContext context)
    {
        MemoryStream mem = new MemoryStream();

        using (Stream stream = await context.OpenAssetStreamAsync().ConfigureAwait(false))
        {   
            await stream.CopyToAsync(mem);
        }

        mem.Seek(0, SeekOrigin.Begin);

        PdfMetadata metadata = new PdfMetadata();

        using (PdfDocument document = PdfDocument.Open(mem))
        {
            metadata.CountPages = document.NumberOfPages;
            metadata.IsEncrypted = document.IsEncrypted;
            metadata.PdfVersion = document.Version.ToString(CultureInfo.InvariantCulture);
        }

        await context.SetMetadataAsync(metadata).ConfigureAwait(false);

        return true;
    }
}
