using DragonFly.Content;
using DragonFly.Storage;
using ImageWizard.Core.ImageLoaders;
using ImageWizard.Core.Types;
using ImageWizard.Services.Types;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    /// <summary>
    /// DragonFlyImageLoader
    /// </summary>
    public class DragonFlyImageLoader : ImageLoaderBase
    {
        public DragonFlyImageLoader(IAssetStorage storage)
        {
            Storage = storage;
        }

        /// <summary>
        /// Storage
        /// </summary>
        private IAssetStorage Storage { get; }

        public override async Task<OriginalData?> GetAsync(string source, ICachedImage? existingCachedImage)
        {
            int pos = source.IndexOf("?");

            //remove query parameter
            if (pos != -1)
            {
                source = source.Substring(0, pos);
            }

            Guid id = Guid.Parse(source);

            Asset asset = await Storage.GetAssetAsync(id);

            using Stream stream = await Storage.DownloadAsync(id);

            MemoryStream mem = new MemoryStream();

            await stream.CopyToAsync(mem);

            return new OriginalData(asset.MimeType, mem.ToArray());
        }
    }
}