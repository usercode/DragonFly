using DragonFly.Content;
using DragonFly.Storage;
using ImageWizard;
using ImageWizard.Loaders;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard;

/// <summary>
/// DragonFlyLoader
/// </summary>
public class DragonFlyLoader : Loader<DragonFlyLoaderOptions>
{
    public DragonFlyLoader(IAssetStorage storage, IOptions<DragonFlyLoaderOptions> options)
        : base(options)
    {
        Storage = storage;
    }

    /// <summary>
    /// Storage
    /// </summary>
    private IAssetStorage Storage { get; }

    public override async Task<LoaderResult> GetAsync(string source, ICachedData? existingCachedImage)
    {
        int pos = source.IndexOf("?");

        Guid id;

        //remove query parameter
        if (pos != -1)
        {
            id = Guid.Parse(source.AsSpan(0, pos));
        }
        else
        {
            id = Guid.Parse(source);
        }            

        Asset asset = await Storage.GetAssetAsync(id);

        Stream stream = await Storage.DownloadAsync(id);        

        return LoaderResult.Success(new OriginalData(asset.MimeType, stream));
    }
}
