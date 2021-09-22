using DragonFly.Assets;
using DragonFly.Content;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;

namespace DragonFly.AspNetCore
{
    /// <summary>
    /// PdfAssetProcessing
    /// </summary>
    public class PdfAssetProcessing : IAssetProcessing
    {
        public bool CanUse(Asset asset)
        {
            return asset.IsPdf();
        }

        public async Task OnAssetChangedAsync(IAssetProcessingContext context)
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

            await context.AddMetadataAsync(metadata);
        }
    }
}
